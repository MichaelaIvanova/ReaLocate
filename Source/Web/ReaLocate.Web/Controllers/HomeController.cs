using ReaLocate.Web.Models;
using ReaLocate.Web.TestServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReaLocate.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestService testService;
        public HomeController(ITestService testService)
        {
            this.testService = testService;
        }
        public ActionResult Index()
        {
            var data = this.testService.GetData();

            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}