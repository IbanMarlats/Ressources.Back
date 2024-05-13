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
    internal class CategoryTest
    {
        private CategoryController _controller;
        private Mock<ICategoryRepository> _mockCategoryRepository;

        [SetUp]
        public void Setup()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();

            _controller = new CategoryController(_mockCategoryRepository.Object);
        }

        [Test]
        public void Post()
        {
            var newCategory = new CategoryModel { Id = 3, Libelle = "newCategory"};
            _mockCategoryRepository.Setup(repo => repo.Create(newCategory)).Returns(newCategory);

            var result = _controller.Post(newCategory);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(newCategory, okResult.Value);
        }

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
    }
}
