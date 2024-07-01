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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public CategoryController(ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        public ActionResult<IEnumerable<CategoryModel>> Get()
        {
            var categories = _categoryRepository.Read();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult<CategoryModel> GetCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            return Ok(category);
        }

        [HttpPost]
        [EnableCors("AllowOrigin")]
        public ActionResult<CategoryModel> Post([FromBody] CategoryModel model, [FromHeader] int currentUserId)
        {
            var currentuser = GetCurrentUser(currentUserId);

            if (currentuser == null || currentuser.IdTypeUser != 2)
            {
                return Unauthorized();
            }

            var category = _categoryRepository.Create(model);
            return Ok(category);
        }

        [HttpPut("{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult Put(int id, [FromBody] CategoryModel model)
        {
            _categoryRepository.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        [EnableCors("AllowOrigin")]
        public ActionResult Delete(int id)
        {
            _categoryRepository.Delete(id);
            return Ok();
        }

        private UserModel GetCurrentUser(int userId)
        {
            return _userRepository.GetUserById(userId);
        }
    }
}

