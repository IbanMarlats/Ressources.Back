using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ressources.Back.Data.Models;

namespace Ressources.Back.Data.Repositories
{
    public interface ICategoryRepository
    {
        CategoryModel Create(CategoryModel Category);
        IEnumerable<CategoryModel> Read();
        CategoryModel GetCategoryById(int id);
        void Update(int id, CategoryModel Category);
        void Delete(int id);
    }
}
