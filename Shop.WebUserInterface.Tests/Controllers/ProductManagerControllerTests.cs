using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Core.Logic;
using Shop.Core.Models;
using Shop.Core.ViewModels;
using Shop.WebUserInterface.Controllers;
using Shop.WebUserInterface.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Shop.WebUserInterface.Tests.Controllers
{
    [TestClass]
    public class ProductManagerControllerTests
    {
        IRepository<Product> context;
        IRepository<ProductCategory> categoryContext;

        [TestInitialize]
        public void Setup()
        {
            context = new MockContext<Product>();
            categoryContext = new MockContext<ProductCategory>();
        }

        [TestMethod]
        [TestCategory("Product Manager Controller")]
        public void IndexAction_DoesReturn_ListOfProducts()
        {
            //Arrange
            context.Insert(new Product());
            context.Insert(new Product());

            //Act
            ProductManagerController controller = new ProductManagerController(context, categoryContext);
            var result = controller.Index() as ViewResult;
            var viewModel = (List<Product>)result.ViewData.Model;

            //Assert
            Assert.AreEqual(2, viewModel.Count());
        }

        [TestMethod]
        [TestCategory("Product Manager Controller")]
        public void CreateAction_DoesReturn_ProductAndListOfProducts()
        {
            //Arrange
            context.Insert(new Product());
            categoryContext.Insert(new ProductCategory());
            categoryContext.Insert(new ProductCategory());

            //Act
            ProductManagerController controller = new ProductManagerController(context, categoryContext);
            var result = controller.Create() as ViewResult;
            var viewModel = (ProductCategoryViewModel)result.ViewData.Model;

            //Assert
            Assert.IsNotNull(viewModel.Product);
            Assert.AreEqual(2, viewModel.ProductCategories.Count());
        }

        [TestMethod]
        [TestCategory("Product Manager Controller")]
        public void CreateWithHttpPostedFile_DoesInsertProductAndImage()
        {
            //Arrange
            string filePath = Path.GetFullPath(@"c:\temp\image.jpg");
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            MyFileBase fileImage = new MyFileBase(fileStream, "image/jpeg", "image.jpg");

            //Act
            ProductManagerController controller = new ProductManagerController(context, categoryContext);
            var result = controller.Create(new Product { Id = 1 }, fileImage) as ViewResult;

            //Assert
            Assert.IsNotNull(context.FindById(1));

        }
    }
}
