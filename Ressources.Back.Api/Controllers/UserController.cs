using BCrypt.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Ressources.Back.Data.Models;
using Ressources.Back.Data.Repositories;
using System;
using System.Collections.Generic;

namespace Ressources.Back.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        public ActionResult<IEnumerable<UserModel>> Get()
        {
            var users = _userRepository.Read();
            foreach (var user in users)
            {
                user.DecryptData();
            }
            return Ok(users);
        }

        [HttpGet("byId/{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<UserModel> GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user != null)
            {
                user.DecryptData();
            }
            return Ok(user);
        }

        [HttpGet("{login}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<UserModel> GetUserByLogin(string login)
        {
            var user = _userRepository.GetUserByLogin(login);
            if (user != null)
            {
                user.DecryptData();
            }
            return Ok(user);
        }

        [HttpPost]
        [EnableCors("AllowOrigin")]
        public ActionResult<UserModel> Post([FromBody] UserModel model)
        {
            model.Mdp = HashPassword(model.Mdp);
            model.EncryptData();
            return Ok(_userRepository.Create(model));
        }

        [HttpPut("{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult Put(int id, [FromBody] UserModel model, [FromHeader] int currentUserId)
        {
            var currentuser = GetCurrentUser(currentUserId);

            if (currentuser == null || currentuser.IdTypeUser != 4)
            {
                return Unauthorized();
            }

            model.EncryptData();
            _userRepository.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult Delete(int id, [FromHeader] int currentUserId)
        {
            var currentuser = GetCurrentUser(currentUserId);

            if (currentuser == null || currentuser.IdTypeUser != 4)
            {
                return Unauthorized();
            }

            _userRepository.Delete(id);
            return Ok();
        }

        [HttpPost("authenticate")]
        [EnableCors("AllowOrigin")]
        public ActionResult<UserModel> Authenticate([FromBody] UserModel userModel)
        {
            var user = _userRepository.GetUserByLogin(userModel.Login);

            if (user == null || !VerifyPassword(userModel.Mdp, user.Mdp))
            {
                return StatusCode(400);
            }

            if (user.Activate == 0)
            {
                throw new Exception("Votre compte est désactivé, veuillez contacter le support.");
            }

            user.DecryptData();
            return Ok(user);
        }

        private UserModel GetCurrentUser(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user != null)
            {
                user.DecryptData();
            }
            return user;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
        }
    }
}
