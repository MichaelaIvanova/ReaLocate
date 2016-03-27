using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReaLocate.Web.Controllers;
using System.Web.Mvc;

namespace ReaLocate.Web.Controllers.Tests
{
    [TestClass()]
    public class ErrorControllerTests
    {
        ErrorController controller;

        [TestInitialize]
        public void SetUp()
        {
            controller = new ErrorController();
        }

        [TestMethod()]
        public void ErrorAgencyTest()
        {
            var result = controller.ErrorAgency() as ContentResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ErrorBrokerTest()
        {
            var result = controller.ErrorBroker() as ContentResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ErrorAdminTest()
        {
            var result = controller.ErrorAdmin() as ContentResult;
            Assert.IsNotNull(result);
        }

        //[TestMethod()]
        //public void NotFoundTest()
        //{
        //    var result = controller.NotFound() as ViewResult;
        //    Assert.AreEqual("NotFound", result.ViewName);
        //}

        //[TestMethod()]
        //public void ErrorUserAlredyIsInAgencyTest()
        //{
        //    var result = controller.ErrorUserAlredyIsInAgency() as ActionResult;
        //    Assert.IsNotNull(result);
        //}
    }
}