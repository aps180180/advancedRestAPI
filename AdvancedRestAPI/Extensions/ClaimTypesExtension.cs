using System.Security.Claims;

namespace AdvancedRestAPI.Extensions
{
    public static class ClaimTypesExtension
    {
        public static string Id(this ClaimsPrincipal user)
        {
			try
			{
				return user.Claims.FirstOrDefault(x => x.Type == "id").Value ?? string.Empty;
			}
			catch 
			{

				return string.Empty;	
			}
        }

        public static string Name(this ClaimsPrincipal user)
        {
            try
            {
                return user.Claims.FirstOrDefault(x => x.Type == "email").Value ?? string.Empty;
            }
            catch
            {

                return string.Empty;
            }
        }

        public static string GivenName(this ClaimsPrincipal user)
        {
            try
            {
                return user.Claims.FirstOrDefault(x => x.Type == "name").Value ?? string.Empty;
            }
            catch
            {

                return string.Empty;
            }
        }

        public static string Email(this ClaimsPrincipal user)
        {
            try
            {
                return user.Claims.FirstOrDefault(x => x.Type == "email").Value ?? string.Empty;
            }
            catch
            {

                return string.Empty;
            }
        }
    }
}
