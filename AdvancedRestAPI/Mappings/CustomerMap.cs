using AdvancedRestAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvancedRestAPI.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(x => x.Id);  
            
            builder.Property(x=> x.Name)
                   .HasColumnName("Name")
                   .HasColumnType("NVARCHAR")
                   .HasMaxLength(150)
                   .IsRequired(true);
            
            builder.Property(x => x.Address)
                   .HasColumnName("Address")
                   .HasColumnType("NVARCHAR")
                   .HasMaxLength(250)
                   .IsRequired(true);

            builder.Property(x => x.Phone)
                   .HasColumnName("Phone")
                   .HasColumnType("NVARCHAR")
                   .HasMaxLength(20);
            
            builder.Property(x => x.BloodGroup)
                   .HasColumnName("BloodGroup")
                   .HasColumnType("NVARCHAR")
                   .HasMaxLength(10);

           



        }
    }
}
