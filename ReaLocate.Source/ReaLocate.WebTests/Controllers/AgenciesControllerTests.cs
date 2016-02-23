namespace ReaLocate.Web.Controllers.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ReaLocate.Services.Data;
    using ReaLocate.Services.Data.Contracts;
    using ReaLocate.Web.Controllers;
    using ReaLocate.Web.Helpers;
    using ReaLocate.Web.ViewModels;
    using System.Web.Mvc;

    [TestClass()]
    public class AgenciesControllerTests
    {
        Mock<IRealEstatesService> realEstatesServiceMock;
        Mock<IPhotosService> photosServiceMock;
        Mock<IVisitorsService> visitorsServiceMock;
        Mock<IUsersService> usersServiceMock;
        Mock<IUsersRolesService> rolesServiceMock;
        Mock<IRealEstateCreateUtil> utilMock;
        Mock<IAgenciesService> agenciesMock;
        Mock<IPaymentDetailsService> paymentMock;
        Mock<IInvoicesService> invoiceMock;

        AgenciesController controller;

        [TestInitialize]
        public void SetUp()
        {
            realEstatesServiceMock = new Mock<IRealEstatesService>();
            agenciesMock = new Mock<IAgenciesService>();
            rolesServiceMock = new Mock<IUsersRolesService>();
            usersServiceMock = new Mock<IUsersService>();
            paymentMock = new Mock<IPaymentDetailsService>();
            invoiceMock = new Mock<IInvoicesService>();

            controller = new AgenciesController(usersServiceMock.Object, agenciesMock.Object
                , paymentMock.Object, rolesServiceMock.Object, invoiceMock.Object);
        }

        [TestMethod()]
        public void CreateAgencyTest()
        {
            var result = controller.CreateAgency(new CreateAgencyViewModel()) as ActionResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetMyAgencyTest()
        {
            var result = controller.GetMyAgency() as RedirectToRouteResult;
            Assert.IsNotNull(result.RouteName);
        }

        [TestMethod()]
        public void AgencyDetailsTest()
        {
            var result = controller.AgencyDetails("MTYuMTIzMTIzMTMxMjM%3d") as ViewResult;
            Assert.IsNotNull(result.View);
        }
    }
}