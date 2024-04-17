using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Ressources.Back.Data.Models;
using Ressources.Back.Data.Repositories;

namespace Ressources.Back.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            this.categoryRepository = categoryRepository;
        }
        [HttpGet]
        [EnableCors("AllowOrigin")]
        public ActionResult<IEnumerable<CategoryModel>> Get()
        {
            var categorys = categoryRepository.Read();
            return Ok(categorys);
        }
        [HttpGet("{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<CategoryModel> GetCategoryById(int id)
        {
            var category = categoryRepository.GetCategoryById(id);
            return Ok(category);
        }
        [HttpPost]
        [EnableCors("AllowOrigin")]
        public ActionResult<CategoryModel> Post([FromBody] CategoryModel model)
        {
            return Ok(categoryRepository.Create(model));
        }
        [HttpPut("{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult Put(int id, CategoryModel model)
        {
            categoryRepository.Update(id, model);
            return Ok();
        }
        [HttpDelete("{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult Delete(int id)
        {
            categoryRepository.Delete(id);
            return Ok();
        }
    }
}
