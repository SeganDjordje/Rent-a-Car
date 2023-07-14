using System.Web;
using System.Web.Mvc;

namespace RentalCars
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // Global Authorization
            filters.Add(new AuthorizeAttribute());

            filters.Add(new RequireHttpsAttribute()); // Added, Face, Twitter etc
        }
    }
}
