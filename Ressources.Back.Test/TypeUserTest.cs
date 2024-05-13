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
    [TestFixture]
    public class TypeUserTest
    {
        private TypeUserController controller;
        private Mock<ITypeUserRepository> mockTypeUserRepository;

        [SetUp]
        public void Setup()
        {
            mockTypeUserRepository = new Mock<ITypeUserRepository>();
            controller = new TypeUserController(mockTypeUserRepository.Object);
        }
        [Test]
        public void Post()
        {
            var newTypeUser = new TypeUserModel { Id = 100, Libelle = "Moderateur" };
            mockTypeUserRepository.Setup(repo => repo.Create(newTypeUser)).Returns(newTypeUser);
            var result = controller.Post(newTypeUser);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(newTypeUser, okResult.Value);
        }
        [Test]
        public void Put()
        {
            var typeUserId = 1;
            var updatedTypeUser = new TypeUserModel { Id = typeUserId, Libelle = "updateduser"};

            var result = controller.Put(typeUserId, updatedTypeUser);

            Assert.IsInstanceOf<OkResult>(result);
        }
        [Test]
        public void Delete()
        {
            var typeUserId = 1;

            var result = controller.Delete(typeUserId);

            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}
