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
    public class RessourceTest
    {
        private RessourceController _controller;
        private Mock<IRessourceRepository> _mockRessourceRepository;

        [SetUp]
        public void Setup()
        {
            _mockRessourceRepository = new Mock<IRessourceRepository>();

            _controller = new RessourceController(_mockRessourceRepository.Object);
        }

        [Test]
        public void Post()
        {

        var newRessource = new RessourceModel { Id = 3, Titre = "newRessource", IdCategory = 2, IdUser = 15 };
            _mockRessourceRepository.Setup(repo => repo.Create(newRessource)).Returns(newRessource);

            var result = _controller.Post(newRessource);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(newRessource, okResult.Value);
        }

        [Test]
        public void Put()
        {
            var RessourceId = 1;
            var updatedRessource = new RessourceModel { Id = RessourceId, Titre = "newRessource", IdCategory = 2, IdUser = 15 };

            var result = _controller.Put(RessourceId, updatedRessource);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Delete()
        {
            var RessourceId = 1;

            var result = _controller.Delete(RessourceId);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void GetById()
        {
            var ressourceId = 1;
            var expectedRessource = new RessourceModel { Id = ressourceId, Titre = "Test Ressource", IdCategory = 2, IdUser = 15 };
            _mockRessourceRepository.Setup(repo => repo.GetRessourceById(ressourceId)).Returns(expectedRessource);

            var result = _controller.GetRessourceById(ressourceId);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(expectedRessource, okResult.Value);
        }

        [Test]
        public void Get()
        {
            var expectedRessources = new List<RessourceModel>
            {
                new RessourceModel { Id = 1, Titre = "Ressource 1", IdCategory = 2, IdUser = 15 },
                new RessourceModel { Id = 2, Titre = "Ressource 2", IdCategory = 3, IdUser = 16 }
            };
            _mockRessourceRepository.Setup(repo => repo.Read()).Returns(expectedRessources);

            var result = _controller.Get();

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var ressources = okResult.Value as List<RessourceModel>;
            Assert.IsNotNull(ressources);
            Assert.AreEqual(expectedRessources.Count, ressources.Count);
            for (int i = 0; i < expectedRessources.Count; i++)
            {
                Assert.AreEqual(expectedRessources[i], ressources[i]);
            }
        }
    }
}
