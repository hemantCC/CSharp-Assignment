using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Assignment.MVC.Controllers;
using Assignment.MVC.MappingProfiles;
using Assignment.MVC.Models.BusinessEntities;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.IO;

namespace Assignment.MVC.Tests
{
    [TestClass]
    public class ProductControllerTest
    {
        public ProductControllerTest()
        {
            //Initialise Mapper Profile
            Mapper.Initialize(cfg => cfg.AddProfile<AutomapperProfile>());
        }

        [TestMethod]
        public async Task Index_IsValidUser_ReturnsView()
        {
            //Arrange
            ProductController controller = new ProductController();
            string ExpectedView = "Index";

            //Act
            var Result = await controller.Index() as ViewResult;

            //Assert
            Assert.IsInstanceOfType(Result, typeof(ViewResult));
            Assert.AreEqual(ExpectedView, Result.ViewName);
        }

        //Checks if we get values 
        [TestMethod]
        public async Task Index_ValidUser_ReturnsValue()
        {
            //Arrange
            ProductController controller = new ProductController();

            //Act
            var Result = await controller.Index() as ViewResult;
            List<ProductVM> products = (List<ProductVM>)Result.Model;

            //Assert
            Assert.IsNotNull(products);

        }


        //For Edit existing Product
        [TestMethod]
        public async Task AddOrEditProduct_CorrectInput_ReturnsViewResult()
        {
            //Arrange
            ProductController controller = new ProductController();
            ProductVM Input = new ProductVM() { Id = 5, UserId = 1, Name = "Iphone 7", Category = "Electronics", Code = "E1", Price = 120000, Description = "This product is value for money.", Status = "In Stock", Discount = 10000 }; //Correct Input
            string ExpectedAction = "Index";
            string ExpectedController = "Product";

            //Act
            var Result = await controller.AddOrEditProduct(Input) as RedirectToRouteResult;
            Result.RouteValues["action"].Equals("Index");
            Result.RouteValues["controller"].Equals("Product");

            //Assert
            Assert.AreEqual(ExpectedAction, Result.RouteValues["action"]);
            Assert.AreEqual(ExpectedController, Result.RouteValues["controller"]);


        }

        [TestMethod]
        public async Task DeleteMultiple_CorrectInput_ReturnsRedirectToRouteResult()
        {
            //Arrange
            ProductController controller = new ProductController();
            string ExpectedAction = "Index";
            string ExpectedController = "Product";

            //Act
            var form = new FormCollection {{"DeleteId", "5"}};
            var Result = await controller.DeleteMultiple(form) as RedirectToRouteResult;
            Result.RouteValues["action"].Equals("Index");
            Result.RouteValues["controller"].Equals("Product");

            //Assert
            Assert.AreEqual(ExpectedAction, Result.RouteValues["action"]);
            Assert.AreEqual(ExpectedController, Result.RouteValues["controller"]);


        }

        [TestMethod]
        public async Task DeleteMultiple_IncorrectInput_ReturnsTempData()
        {
            //Arrange
            ProductController controller = new ProductController();

            //Act
            var form = new FormCollection {};
            var Result = await controller.DeleteMultiple(form) as ViewResult;

            //Assert
            Assert.AreEqual("NoDeleteItems", Result.TempData["NoDeleteItems"] as string);


        }



    }



}
