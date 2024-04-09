using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Ressources.Back.Data.Models;
using Ressources.Back.Data.Repositories;

namespace Ressources.Back.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VuController : ControllerBase
    {
        private readonly IVuRepository vuRepository;
        private readonly IVuRepository _vuRepository;
        public VuController(IVuRepository vuRepository)
        {
            _vuRepository = vuRepository ?? throw new ArgumentNullException(nameof(vuRepository));
            this.vuRepository = vuRepository;
        }
        [HttpGet]
        [EnableCors("AllowOrigin")]
        public ActionResult<IEnumerable<VuModel>> Get()
        {
            var vus = vuRepository.Read();
            return Ok(vus);
        }
        
        [HttpPost]
        [EnableCors("AllowOrigin")]
        public ActionResult<VuModel> Post([FromQuery] VuModel model)
        {
            return Ok(vuRepository.Create(model));
        }
        [HttpPut("{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult Put(int id, VuModel model)
        {
            vuRepository.Update(id, model);
            return Ok();
        }
        [HttpDelete]
        [EnableCors("AllowOrigin")]
        public ActionResult Delete(int idUser, int idRessource)
        {
            vuRepository.Delete(idUser, idRessource);
            return Ok();
        }
    }
}
