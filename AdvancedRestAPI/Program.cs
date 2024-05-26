using AdvancedRestAPI.Data;
using AdvancedRestAPI.Interfaces;
using AdvancedRestAPI.Models;
using AdvancedRestAPI.Profiles;
using AdvancedRestAPI.Services;
using AdvancedRestAPI.Validators;
using AspNetCoreRateLimit;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddOData( option => option.Select().Filter().OrderBy());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Fluent Validation Services
builder.Services.AddValidatorsFromAssemblyContaining<CustomerDTOValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
#endregion

#region Database Services
builder.Services.AddDbContext<AppDbContext>(options =>
{

    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
#endregion

#region DI Services
builder.Services.AddAutoMapper(typeof(AutomapperConfig));
builder.Services.AddScoped<ICustomer, CustomerService>();
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>((options) =>
{
    options.GeneralRules = new List<RateLimitRule>()
    {
        new RateLimitRule()
        {
            Endpoint ="*",
            Limit = 50,
            Period = "3m"       

        }
    };
});
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

#endregion

#region Carrega JWTSettings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var key = builder.Configuration.GetSection("JwtSettings").GetValue<string>("PrivateKey");
builder.Services.AddTransient<TokenService>();
#endregion

#region Authentication and Authorization services
builder.Services.AddAuthentication(x=>
{

    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x=>
{
    x.TokenValidationParameters =
    new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false,
        
    };
});




    builder.Services.AddAuthorization();
#endregion

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();


app.UseIpRateLimiting();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
