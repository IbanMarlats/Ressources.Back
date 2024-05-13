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
    internal class PublicationTest
    {
        private PublicationController _controller;
        private Mock<IPublicationRepository> _mockPublicationRepository;

        [SetUp]
        public void Setup()
        {
            _mockPublicationRepository = new Mock<IPublicationRepository>();

            _controller = new PublicationController(_mockPublicationRepository.Object);
        }

        [Test]
        public void Post()
        {      
            var newPublication = new PublicationModel { Id = 3, Titre = "newPublication", Contenu="blablabla", NbLike=3, IdRessource=3 };
            _mockPublicationRepository.Setup(repo => repo.Create(newPublication)).Returns(newPublication);

            var result = _controller.Post(newPublication);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(newPublication, okResult.Value);
        }

        [Test]
        public void Put()
        {
            var PublicationId = 1;
            var updatedPublication = new PublicationModel { Id = PublicationId, Titre = "newPublication", Contenu = "blablabla", NbLike = 3, IdRessource = 3 };

            var result = _controller.Put(PublicationId, updatedPublication);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Delete()
        {
            var PublicationId = 1;

            var result = _controller.Delete(PublicationId);

            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}
