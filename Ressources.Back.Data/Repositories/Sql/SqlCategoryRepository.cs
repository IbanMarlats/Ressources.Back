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

            if (category != null)
            {
                // Accès sécurisé aux propriétés de commentaire car commentaire n'est pas null
                Console.WriteLine($"La categorie est trouvé");
            }
            else
            {
                // Gérer le cas où aucun commentaire n'est trouvé avec l'identifiant spécifié
                Console.WriteLine("Aucune catégorie trouvée avec cet identifiant.");
            }

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
        public void Delete(int id)
        {
            var model = context.Category.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return;
            context.Category.Remove(model);
            context.SaveChanges();
        }
    }
}
