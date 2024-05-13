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
    internal class RessourceTest
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
    }
}
