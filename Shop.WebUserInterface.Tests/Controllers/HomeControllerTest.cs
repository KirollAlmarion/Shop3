using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Core.Logic;
using Shop.Core.Models;
using Shop.Core.ViewModels;
using Shop.WebUserInterface;
using Shop.WebUserInterface.Controllers;
using Shop.WebUserInterface.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Shop.WebUserInterface.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        [TestCategory("Home Controller")]
        public void Index_DoesReturn_Product()
        {
            IRepository<Product> context = new MockContext<Product>();
            IRepository<ProductCategory> categoryContext = new MockContext<ProductCategory>();
            HomeController controller = new HomeController(context, categoryContext);

            context.Insert(new Product());

            var result = controller.Index() as ViewResult;
            var viewModel = (ProductListViewModel)result.ViewData.Model;

            Assert.AreEqual(1, viewModel.Products.Count);
            //Assert.AreEqual(1, viewModel.Products.Count());//si ligne précésente ne fonctionne pas
        }
    }
}
