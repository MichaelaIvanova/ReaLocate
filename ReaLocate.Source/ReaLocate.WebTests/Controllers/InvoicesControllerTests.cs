using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReaLocate.Services.Data;
using ReaLocate.Services.Data.Contracts;
using ReaLocate.Web.Controllers;
using ReaLocate.Web.Helpers;
using System.Web.Mvc;

namespace ReaLocate.Web.Controllers.Tests
{
    [TestClass()]
    public class InvoicesControllerTests
    {
        Mock<IRealEstatesService> realEstatesServiceMock;
        Mock<IPhotosService> photosServiceMock;
        Mock<IVisitorsService> visitorsServiceMock;
        Mock<IUsersService> usersServiceMock;
        Mock<IUsersRolesService> rolesServiceMock;
        Mock<IRealEstateCreateUtil> utilMock;
        Mock<IAgenciesService> agenciesMock;
        Mock<IInvoicesService> invoiceMock;

        InvoicesController controller;

        [TestInitialize]
        public void SetUp()
        {
            realEstatesServiceMock = new Mock<IRealEstatesService>();
            agenciesMock = new Mock<IAgenciesService>();
            rolesServiceMock = new Mock<IUsersRolesService>();
            usersServiceMock = new Mock<IUsersService>();
            invoiceMock = new Mock<IInvoicesService>();

            controller = new InvoicesController(usersServiceMock.Object, invoiceMock.Object,agenciesMock.Object);
        }


        [TestMethod()]
        public void CreateInvoiceRegularUserTest()
        {
            var result = controller.CreateInvoiceRegularUser("cf12ce0c-541b-46b7-822f-7af3e73b2f7b") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void InvoiceDetailsByIntIdTest()
        {
            var result = controller.InvoiceDetailsByIntId("1") as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void PrintInvoiceTest()
        {
            var result = controller.PrintInvoice("/n/t") as ActionResult;

            Assert.IsNull(result);
        }


        [TestMethod()]
        public void CreateInvoiceForOneOfferTest()
        {
            var result = controller.CreateInvoiceForOneOffer("MTYuMTIzMTIzMTMxMjM%3d") as RedirectResult;

            Assert.IsNotNull(result);
        }
    }
}