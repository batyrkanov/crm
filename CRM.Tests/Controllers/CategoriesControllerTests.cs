using System;
using System.Web.Mvc;
using CRM.Controllers;
using NUnit.Framework;
using Moq;
using CRM.Models;
using CRM;
using System.Collections.Generic;
using System.Data.Entity;
using PagedList;

namespace CRM.Tests.Controllers
{
    [TestFixture]
    public class CategoriesControllerTests
    {
        //[Test]
        //public void IndexReturnsActionResult()
        //{
        //    // Arrange
        //    CategoriesController controller = new CategoriesController();
        //    // Act
        //    var actual = controller.Index(1);

        //    // Assert
        //    Assert.IsInstanceOf<ActionResult>(actual);
        //}

        [Test]
        public void CategoriesIndexViewNotNull()
        {
            var guid = Guid.NewGuid();
            Mock<ICategoryRepository> mock = new Mock<ICategoryRepository>();
            //mock.Setup(m => m.categories).Returns(new Category());
            CategoriesController controller = new CategoriesController();
            var actual = (IPagedList<Category>)controller.Index(1).Model;
            Assert.IsInstanceOf<PagedList<Category>>(actual);
        }

        [Test]
        public async System.Threading.Tasks.Task CreatePostAction_ModelErrorAsync()
        {
            // arrange
            var mock = new Mock<ApplicationDbContext>();
            Category category = new Category();
            CategoriesController controller = new CategoriesController();
            // act
            ActionResult result = await controller.Create(category) as ActionResult;
            // assert
            Assert.IsNotNull(result);
        }
    }
}
