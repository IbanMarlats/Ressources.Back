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

        [Test]
        public void GetById()
        {
            var publicationId = 1;
            var expectedPublication = new PublicationModel { Id = publicationId, Titre = "Test Publication", Contenu = "Test content", NbLike = 5, IdRessource = 10 };
            _mockPublicationRepository.Setup(repo => repo.GetPublicationById(publicationId)).Returns(expectedPublication);

            var result = _controller.GetPublicationById(publicationId);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(expectedPublication, okResult.Value);
        }

        [Test]
        public void GetPublicationByRessourceId()
        {
            var ressourceId = 10;
            var expectedPublications = new List<PublicationModel>
            {
                new PublicationModel { Id = 1, Titre = "Publication 1", Contenu = "Contenu 1", NbLike = 5, IdRessource = ressourceId },
                new PublicationModel { Id = 2, Titre = "Publication 2", Contenu = "Contenu 2", NbLike = 3, IdRessource = ressourceId }

            };
            _mockPublicationRepository.Setup(repo => repo.GetPublicationsByRessourceId(ressourceId)).Returns(expectedPublications);

            var result = _controller.GetPublicationsByRessourceId(ressourceId);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var publications = okResult.Value as List<PublicationModel>;
            Assert.IsNotNull(publications);
            Assert.AreEqual(expectedPublications.Count, publications.Count);
            for (int i = 0; i < expectedPublications.Count; i++)
            {
                Assert.AreEqual(expectedPublications[i], publications[i]);
            }
        }
    }
}
