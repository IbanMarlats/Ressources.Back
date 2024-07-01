using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ressources.Back.Api.Controllers;
using Ressources.Back.Data.Models;
using Ressources.Back.Data.Repositories;

namespace Ressources.Back.Test
{
    public class CategoryTest
    {
        private CategoryController _controller;
        private Mock<ICategoryRepository> _mockCategoryRepository;
        private Mock<IUserRepository> _mockUserRepository;

        [SetUp]
        public void Setup()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockUserRepository = new Mock<IUserRepository>();

            _controller = new CategoryController(_mockCategoryRepository.Object, _mockUserRepository.Object);
        }

        //[Test]
        //public void Post_Administrator()
        //{
        //    var newCategory = new CategoryModel { Id = 3, Libelle = "TestCategorie" };
        //    _mockCategoryRepository.Setup(repo => repo.Create(newCategory)).Returns(newCategory);

        //    var adminUser = new UserModel { IdTypeUser = 2 };

        //    var result = _controller.Post(newCategory, adminUser);

        //    Assert.IsInstanceOf<OkObjectResult>(result.Result);
        //    var okResult = result.Result as OkObjectResult;
        //    Assert.AreEqual(newCategory, okResult.Value);
        //}
        //[Test]
        //public void Post_NonAdministrator()
        //{
        //    var newCategory = new CategoryModel { Id = 4, Libelle = "newCategory" };
        //    var nonAdminUser = new UserModel { IdTypeUser = 1 };

        //    var result = _controller.Post(newCategory, nonAdminUser);

        //    Assert.IsInstanceOf<UnauthorizedResult>(result.Result);
        //}

        [Test]
        public void Put()
        {
            var categoryId = 1;
            var updatedCategory = new CategoryModel { Id = categoryId, Libelle = "newCategory" };

            var result = _controller.Put(categoryId, updatedCategory);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Delete()
        {
            var categoryId = 1;

            var result = _controller.Delete(categoryId);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void GetById()
        {
            var categoryId = 1;
            var expectedCategory = new CategoryModel { Id = categoryId, Libelle = "Test Category" };
            _mockCategoryRepository.Setup(repo => repo.GetCategoryById(categoryId)).Returns(expectedCategory);

            var result = _controller.GetCategoryById(categoryId);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(expectedCategory, okResult.Value);
        }

        [Test]
        public void Get()
        {
            var expectedCategories = new List<CategoryModel>
            {
                new CategoryModel { Id = 1, Libelle = "Category 1" },
                new CategoryModel { Id = 2, Libelle = "Category 2" }
            };
            _mockCategoryRepository.Setup(repo => repo.Read()).Returns(expectedCategories);

            var result = _controller.Get();

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var categories = okResult.Value as List<CategoryModel>;
            Assert.IsNotNull(categories);
            Assert.AreEqual(expectedCategories.Count, categories.Count);
            for (int i = 0; i < expectedCategories.Count; i++)
            {
                Assert.AreEqual(expectedCategories[i], categories[i]);
            }
        }
    }
}
