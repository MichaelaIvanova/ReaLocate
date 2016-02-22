using System.Web.Mvc;

namespace ReaLocate.Web.Areas.AgencyAdministration
{
    public class AgencyAdministrationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AgencyAdministration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AgencyAdministration_default",
                "AgencyAdministration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}