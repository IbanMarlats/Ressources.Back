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

        [Test]
        public void GetById()
        {
            var typeUserId = 1;
            var expectedTypeUser = new TypeUserModel { Id = typeUserId, Libelle = "Test TypeUser" };
            mockTypeUserRepository.Setup(repo => repo.GetTypeUserById(typeUserId)).Returns(expectedTypeUser);

            var result = controller.GetTypeUserById(typeUserId);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(expectedTypeUser, okResult.Value);
        }

        [Test]
        public void Get()
        {
            var expectedTypeUsers = new List<TypeUserModel>
    {
        new TypeUserModel { Id = 1, Libelle = "TypeUser 1" },
        new TypeUserModel { Id = 2, Libelle = "TypeUser 2" }
    };
            mockTypeUserRepository.Setup(repo => repo.Read()).Returns(expectedTypeUsers);

            var result = controller.Get();

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var typeUsers = okResult.Value as List<TypeUserModel>;
            Assert.IsNotNull(typeUsers);
            Assert.AreEqual(expectedTypeUsers.Count, typeUsers.Count);
            for (int i = 0; i < expectedTypeUsers.Count; i++)
            {
                Assert.AreEqual(expectedTypeUsers[i], typeUsers[i]);
            }
        }

    }
}
