using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Ressources.Back.Data.Models;
using Ressources.Back.Data.Repositories;

namespace Ressources.Back.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationRepository publicationRepository;
        private readonly IPublicationRepository _publicationRepository;
        public PublicationController(IPublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository ?? throw new ArgumentNullException(nameof(publicationRepository));
            this.publicationRepository = publicationRepository;
        }
        [HttpGet]
        [EnableCors("AllowOrigin")]
        public ActionResult<IEnumerable<PublicationModel>> Get()
        {
            var publication = publicationRepository.Read();
            return Ok(publication);
        }
        [HttpGet("{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<PublicationModel> GetPublicationById(int id)
        {
            var publication = publicationRepository.GetPublicationById(id);
            return Ok(publication);
        }
        [HttpGet("Ressource/{ressourceId}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<IEnumerable<PublicationModel>> GetPublicationsByRessourceId(int ressourceId)
        {
            var publications = publicationRepository.GetPublicationsByRessourceId(ressourceId);
            return Ok(publications);
        }
        [HttpPost]
        [EnableCors("AllowOrigin")]
        public ActionResult<PublicationModel> Post([FromBody] PublicationModel model)
        {
            return Ok(publicationRepository.Create(model));
        }
        [HttpPut("{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult Put(int id, PublicationModel model)
        {
            publicationRepository.Update(id, model);
            return Ok();
        }
        [HttpDelete]
        [EnableCors("AllowOrigin")]
        public ActionResult Delete(int id)
        {
            publicationRepository.Delete(id);
            return Ok();
        }
    }
}
