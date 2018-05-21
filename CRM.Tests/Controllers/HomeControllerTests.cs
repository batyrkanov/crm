using System;
using CRM.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
namespace CRM.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController controller;
        private ViewResult result;
        
        [TestInitialize]
        public void SetupContext()
        {
            controller = new HomeController();
            result = controller.Index() as ViewResult;
        }
        [TestMethod]
        public void IndexViewResultNotNull()
        {
            Assert.IsNotNull(result);
        }


    }
}
