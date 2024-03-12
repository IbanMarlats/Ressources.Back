using Microsoft.AspNetCore.Mvc;
using Ressources.Back.Data.Models;
using Ressources.Back.Data.Repositories;

namespace Ressources.Back.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeUserController : ControllerBase
    {
        private readonly ITypeUserRepository typeUserRepository;
        private readonly ITypeUserRepository _typeUserRepository;
        public TypeUserController(ITypeUserRepository typeUserRepository)
        {
            _typeUserRepository = typeUserRepository ?? throw new ArgumentNullException(nameof(typeUserRepository));
            this.typeUserRepository = typeUserRepository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<TypeUserModel>> Get()
        {
            var typeUsers = typeUserRepository.Read();
            return Ok(typeUsers);
        }
        [HttpGet("{id}")]
        public ActionResult<TypeUserModel> GetTypeUserById(int id)
        {
            var typeUser = typeUserRepository.GetTypeUserById(id);
            return Ok(typeUser);
        }
        
        [HttpPost]
        public ActionResult<TypeUserModel> Post([FromQuery] TypeUserModel model)
        {
            return Ok(typeUserRepository.Create(model));
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, TypeUserModel model)
        {
            typeUserRepository.Update(id, model);
            return Ok();
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            typeUserRepository.Delete(id);
            return Ok();
        }
    }
}
