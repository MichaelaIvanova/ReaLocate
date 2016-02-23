namespace ReaLocate.Web.Controllers.Tests
{
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ReaLocate.Web.Controllers;
    using Services.Data;
    using Services.Data.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels;

    [TestClass()]
    public class InviteBrokersControllerTests
    {
        Mock<IRealEstatesService> realEstatesServiceMock;
        Mock<IPhotosService> photosServiceMock;
        Mock<IVisitorsService> visitorsServiceMock;
        Mock<IUsersService> usersServiceMock;
        Mock<IUsersRolesService> rolesServiceMock;
        Mock<IRealEstateCreateUtil> utilMock;
        Mock<IAgenciesService> agenciesMock;

        InviteBrokersController controller;

        [TestInitialize]
        public void SetUp()
        {
            realEstatesServiceMock = new Mock<IRealEstatesService>();
            agenciesMock = new Mock<IAgenciesService>();
            rolesServiceMock = new Mock<IUsersRolesService>();
            usersServiceMock = new Mock<IUsersService>();

            controller = new InviteBrokersController(usersServiceMock.Object, agenciesMock.Object, 
                rolesServiceMock.Object);
        }

        [TestMethod()]
        public void InviteBrokersControllerTest()
        {
            var result = controller.GetAllUsersUserNames() as JsonResult;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void InviteBrokersTest()
        {
            var result = controller.InviteBrokers(new BrokerInputViewModel() {UserName="" }) as ActionResult;

            Assert.IsNull(result);
        }


        [TestMethod()]
        public void GetAllUsersUserNamesTest()
        {
            var result = controller.GetAllUsersUserNames() as ActionResult;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetAllBrokersByAgencyTest()
        {
            var result = controller.GetAllBrokersByAgency("/m/n/t") as RedirectToRouteResult;

            Assert.IsNull(result);
        }
    }
}