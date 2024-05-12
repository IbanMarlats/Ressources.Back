using Moq;
using Ressources.Back.Api.Controllers;
using Ressources.Back.Data.Models;
using Ressources.Back.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

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
            _mockUserRepository = new Mock<IUserRepository>();

            _controller = new UserController(_mockUserRepository.Object);
        }

        [Test]
        public void Post_ReturnsCreatedUser()
        {
            var newUser = new UserModel { Id = 3, Login = "newuser", Mdp = "password", Activate = 1, Age = 25, SituationFamiliale = "Célibataire", CSP = "Étudiant", Loisir = "Musique", Autre = "Rien", IdTypeUser = 1, IdStatus = 1 };
            _mockUserRepository.Setup(repo => repo.Create(newUser)).Returns(newUser);

            var result = _controller.Post(newUser);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(newUser, okResult.Value);
        }

        [Test]
        public void Put_ReturnsOkResult()
        {
            var userId = 1;
            var updatedUser = new UserModel { Id = userId, Login = "updateduser", Mdp = "newpassword", Activate = 1, Age = 30, SituationFamiliale = "Célibataire", CSP = "Salarié", Loisir = "Football", Autre = "Autre", IdTypeUser = 1, IdStatus = 1 };

            var result = _controller.Put(userId, updatedUser);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Delete_ReturnsOkResult()
        {
            var userId = 1;

            var result = _controller.Delete(userId);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void Authenticate_WithValidCredentials_ReturnsUser()
        {
            var user = new UserModel { Id = 1, Login = "user1", Mdp = "password", Activate = 1, Age = 30, SituationFamiliale = "Célibataire", CSP = "Salarié", Loisir = "Football", Autre = "Autre", IdTypeUser = 1, IdStatus = 1 };
            _mockUserRepository.Setup(repo => repo.Authenticate("user1", "password")).Returns(user);

            var result = _controller.Authenticate(new UserModel { Login = "user1", Mdp = "password" });

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(user, okResult.Value);
        }

        [Test]
        public void Authenticate_WithInvalidCredentials_ReturnsBadRequest()
        {
            _mockUserRepository.Setup(repo => repo.Authenticate("invaliduser", "invalidpassword")).Returns((UserModel)null);

            var result = _controller.Authenticate(new UserModel { Login = "invaliduser", Mdp = "invalidpassword" });

            Assert.IsInstanceOf<StatusCodeResult>(result.Result);
            var statusCodeResult = result.Result as StatusCodeResult;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(400, statusCodeResult.StatusCode);
        }
    }
}
