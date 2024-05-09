using NUnit.Framework;
using Moq;
using Ressources.Back.Api.Controllers;
using Ressources.Back.Data.Models;
using Ressources.Back.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace Ressources.Back.Api.Tests
{
    [TestFixture]
    public class UserTest
    {
        private UserController _controller;
        private Mock<IUserRepository> _mockUserRepository;

        [SetUp]
        public void Setup()
        {
            // Initialisation du mock du repository
            _mockUserRepository = new Mock<IUserRepository>();

            // Initialisation du contrôleur avec le mock du repository
            _controller = new UserController(_mockUserRepository.Object);
        }

        [Test]
        public void Post_ReturnsCreatedUser()
        {
            // Arrange
            var newUser = new UserModel { Id = 3, Login = "newuser", Mdp = "password", Activate = 1, Age = 25, SituationFamiliale = "Célibataire", CSP = "Étudiant", Loisir = "Musique", Autre = "Rien", IdTypeUser = 1, IdStatus = 1 };
            _mockUserRepository.Setup(repo => repo.Create(newUser)).Returns(newUser);

            // Act
            var result = _controller.Post(newUser);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(newUser, okResult.Value);
        }

        [Test]
        public void Put_ReturnsOkResult()
        {
            // Arrange
            var userId = 1;
            var updatedUser = new UserModel { Id = userId, Login = "updateduser", Mdp = "newpassword", Activate = 1, Age = 30, SituationFamiliale = "Célibataire", CSP = "Salarié", Loisir = "Football", Autre = "Autre", IdTypeUser = 1, IdStatus = 1 };

            // Act
            var result = _controller.Put(userId, updatedUser);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Delete_ReturnsOkResult()
        {
            // Arrange
            var userId = 1;

            // Act
            var result = _controller.Delete(userId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Authenticate_WithValidCredentials_ReturnsUser()
        {
            // Arrange
            var user = new UserModel { Id = 1, Login = "user1", Mdp = "password", Activate = 1, Age = 30, SituationFamiliale = "Célibataire", CSP = "Salarié", Loisir = "Football", Autre = "Autre", IdTypeUser = 1, IdStatus = 1 };
            _mockUserRepository.Setup(repo => repo.Authenticate("user1", "password")).Returns(user);

            // Act
            var result = _controller.Authenticate(new UserModel { Login = "user1", Mdp = "password" });

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(user, okResult.Value);
        }

        [Test]
        public void Authenticate_WithInvalidCredentials_ReturnsBadRequest()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.Authenticate("invaliduser", "invalidpassword")).Returns((UserModel)null);

            // Act
            var result = _controller.Authenticate(new UserModel { Login = "invaliduser", Mdp = "invalidpassword" });

            // Assert
            Assert.IsInstanceOf<StatusCodeResult>(result.Result);
            var statusCodeResult = result.Result as StatusCodeResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }
    }
}
