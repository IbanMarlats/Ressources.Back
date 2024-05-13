using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Ressources.Back.Data.Models;
using Ressources.Back.Data.Repositories;

namespace Ressources.Back.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.userRepository = userRepository;
        }
        [HttpGet]
        [EnableCors("AllowOrigin")]
        public ActionResult<IEnumerable<UserModel>> Get()
        {
            var users = userRepository.Read();
            return Ok(users);
        }
        [HttpGet("byId/{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<UserModel> GetUserById(int id)
        {
            var user = userRepository.GetUserById(id);
            return Ok(user);
        }
        [HttpGet("{login}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<UserModel> GetUserByLogin(string login)
        {
            var user = userRepository.GetUserByLogin(login);
            return Ok(user);
        }
        [HttpPost]
        [EnableCors("AllowOrigin")]
        public ActionResult<UserModel> Post([FromBody] UserModel model)
        {
            return Ok(userRepository.Create(model));
        }
        [HttpPut("{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult Put(int id,  UserModel model)
        {
            userRepository.Update(id, model);
            return Ok();
        }
        [HttpDelete]
        [EnableCors("AllowOrigin")]
        public ActionResult Delete(int id)
        {
            userRepository.Delete(id);
            return Ok();
        }
        [HttpPost("authenticate")]
        [EnableCors("AllowOrigin")]
        public ActionResult<UserModel> Authenticate([FromBody] UserModel userModel)
        {
            var user = userRepository.Authenticate(userModel.Login, userModel.Mdp) ;

            if (user == null)
            {
                return StatusCode(400);
            }

            return Ok(user);
        }
    }
}
