using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ressources.Back.Data.Models;

namespace Ressources.Back.Data.Repositories.Sql
{
    public class SqlCategoryRepository : ICategoryRepository
    {
        private readonly DataContext context;
        public SqlCategoryRepository(DataContext context)
        {
            this.context = context;
        }
        public IEnumerable<CategoryModel> Read()
        {
            return context.Category.ToList();
        }
        public CategoryModel GetCategoryById(int id)
        {
            CategoryModel? category = context.Category.FirstOrDefault(c => c.Id == id);

            return category;

        }
        public CategoryModel Create(CategoryModel model)
        {
            context.Category.Add(model);
            context.SaveChanges();
            return model;
        }
        public void Update(int id, CategoryModel model)
        {
            var existingCategory = context.Category.FirstOrDefault(c => c.Id == id);
            if (existingCategory != null)
            {
                existingCategory.Libelle = model.Libelle;
                context.SaveChanges();
            }
        }
        /*public void Delete(int id)
        {
            var model = context.Category.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return;
            context.Category.Remove(model);
            context.SaveChanges();
        }*/
        
        public void Delete(int id)
        {
            // Recherche de la catégorie par son ID
            var category = context.Category.FirstOrDefault(x => x.Id == id);

            // Vérification si la catégorie existe
            if (category == null)
            {
                // Si la catégorie n'existe pas, vous pouvez lever une exception ou simplement retourner sans rien faire.
                //Dans cet exemple, nous choisissons de retourner sans rien faire.
                return;
            }

            // Vérification s'il existe des ressources associées à cette catégorie
            var resourcesCount = context.Ressource.Count(r => r.IdCategory == id);

            if (resourcesCount > 0)
            {
                // Si des ressources sont associées à cette catégorie, renvoyer une erreur
                throw new Exception("Impossible de supprimer cette catégorie car elle a des ressources associées.");

        }
            else
            {
                // Suppression de la catégorie
                context.Category.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
