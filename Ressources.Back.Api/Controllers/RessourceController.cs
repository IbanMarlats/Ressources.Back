using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Ressources.Back.Data.Models;
using Ressources.Back.Data.Repositories;

namespace Ressources.Back.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RessourceController : ControllerBase
    {
        private readonly IRessourceRepository ressourceRepository;
        private readonly IRessourceRepository _ressourceRepository;
        public RessourceController(IRessourceRepository ressourceRepository)
        {
            _ressourceRepository = ressourceRepository ?? throw new ArgumentNullException(nameof(ressourceRepository));
            this.ressourceRepository = ressourceRepository;
        }
        [HttpGet]
        [EnableCors("AllowOrigin")]
        public ActionResult<IEnumerable<RessourceModel>> Get()
        {
            var ressource = ressourceRepository.Read();
            return Ok(ressource);
        }
        [HttpGet("{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<RessourceModel> GetRessourceById(int id)
        {
            var ressource = ressourceRepository.GetRessourceById(id);
            return Ok(ressource);
        }
        [HttpPost]
        [EnableCors("AllowOrigin")]
        public ActionResult<RessourceModel> Post([FromBody] RessourceModel model)
        {
            return Ok(ressourceRepository.Create(model));
        }
        [HttpPut("{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult Put(int id, RessourceModel model)
        {
            ressourceRepository.Update(id, model);
            return Ok();
        }
        [HttpDelete("{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult Delete(int id)
        {
            ressourceRepository.Delete(id);
            return Ok();
        }
        [HttpGet("search")]
        [EnableCors("AllowOrigin")]
        public ActionResult<IEnumerable<RessourceModel>> GetRessourcesByNameAndCategory([FromQuery] string? searchText, [FromQuery] int? categoryId)
        {
            int categoryIdNonNull = categoryId.HasValue ? categoryId.Value : 0;

            var ressources = ressourceRepository.GetRessourcesByNameAndCategory(searchText, categoryIdNonNull);
            return Ok(ressources);
        }
    }
}
