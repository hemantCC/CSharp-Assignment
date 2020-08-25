using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Assignment.MVC.Controllers;
using Assignment.MVC.MappingProfiles;
using Assignment.MVC.Models.BusinessEntities;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assignment.MVC.Tests
{
    [TestClass]
    public class UserControllerTest
    {
        public UserControllerTest()
        {
            //Initialise Mapper Profile
            Mapper.Initialize(cfg => cfg.AddProfile<AutomapperProfile>());
        }

        [TestMethod]
        public async Task EditProfile_PositiveTest_returnsJson()
        {
            //Arrange
            UserController controller = new UserController();
            ProfileVM Input = new ProfileVM() { 
            Id = 4,
            Name = "Hemant",
            Email = "hemantmohapatra1@gmail.com",
            Contact = "1231231231",
            Address = "Bodakdev",
            City = "Ahmedabad",
            State = "Gujarat",
            Country ="India",
            ZipCode = "123123"
            };
            string ExpectedString = "success";

            //Act
            var Result = await controller.EditProfile(Input) as JsonResult;

            //Assert
            Assert.AreEqual(ExpectedString,Result.Data);
        }

        [TestMethod]
        public async Task EditProfile_NegativeTest_returnsJson()
        {
            //Arrange
            UserController controller = new UserController();
            ProfileVM Input = new ProfileVM()
            {
                Id = 1,
                Name = "Hemant",
                Email = "hemantmohapatra1@gmail.com",
                Contact = "1231231231",
                Address = "Bodakdev",
                City = "Ahmedabad",
                State = "Gujarat",
                Country = "India",
                ZipCode = "123123"
            };
            string ExpectedString = "fail";

            //Act
            var Result = await controller.EditProfile(Input) as JsonResult;

            //Assert
            Assert.AreEqual(ExpectedString, Result.Data);
        }
    }
}
