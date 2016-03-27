namespace ReaLocate.Web.Controllers.Tests
{
    using Data.Models;
    using Helpers;
    using Infrastructure.Mapping;
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
    public class HomeControllerTests
    {
        Mock<IRealEstatesService> realEstatesServiceMock;
        Mock<IPhotosService> photosServiceMock;
        Mock<IVisitorsService> visitorsServiceMock;
        Mock<IUsersService> usersServiceMock;
        Mock<IUsersRolesService> rolesServiceMock;
        Mock<IRealEstateCreateUtil> utilMock;

        HomeController controller;

        [TestInitialize]
        public void SetUp()
        {
            realEstatesServiceMock = new Mock<IRealEstatesService>();
            controller = new HomeController(realEstatesServiceMock.Object);
        }

        [TestMethod]
        public void ByIdShouldWorkCorrectly()
        {
            var autoMapperConfig = new AutoMapperConfig();
            autoMapperConfig.Execute(typeof(RealEstatesController).Assembly);
            const string RealEstateTitle = "SomeTitle";
            var realEstateServiceMock = new Mock<IRealEstatesService>();

            realEstateServiceMock.Setup(x => x.GetByEncodedId(It.IsAny<string>()))
                .Returns(new RealEstate
                {
                    Title = RealEstateTitle,
                    Description = "asghftrjryjrgyhy",
                    Address = "asrhstgyhuhsry"
                });
        }


        [TestMethod]
        public void Index()
        {
            var result = controller.Index(new SearchedRealEstateViewModel()) as ActionResult;

            Assert.IsNotNull(result);
        }


        [TestMethod]

        public void Chat()
        {
            var result = controller.Chat() as ViewResult;

            Assert.IsNotNull(result);
        }


    }
}