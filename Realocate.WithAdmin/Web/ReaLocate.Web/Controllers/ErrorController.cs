using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReaLocate.Web.Controllers
{
    public class ErrorController : BaseController
    {
        private readonly List<string> errorMessages = new List<string>()
        {
            "You alredy own agency",
            "You alredy work for agency, you are a broker",
            "You are admin, cannot post from this profile"
        };

        public ActionResult ErrorAgency()
        {
            return Content(errorMessages[0]);
        }

        public ActionResult ErrorBroker()
        {
            return Content(errorMessages[1]);
        }

        public ActionResult ErrorAdmin()
        {
            return Content(errorMessages[2]);
        }
    }
}