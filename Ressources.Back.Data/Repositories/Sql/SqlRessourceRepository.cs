using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ressources.Back.Data.Models;

namespace Ressources.Back.Data.Repositories.Sql
{
    public class SqlRessourceRepository : IRessourceRepository
    {
        private readonly DataContext context;
        public SqlRessourceRepository(DataContext context)
        {
            this.context = context;
        }
        public IEnumerable<RessourceModel> Read()
        {
            return context.Ressource.ToList();
        }
        public RessourceModel GetRessourceById(int id)
        {
            RessourceModel? ressource = context.Ressource.FirstOrDefault(c => c.Id == id);

            return ressource;

        }
        public RessourceModel Create(RessourceModel model)
        {
            context.Ressource.Add(model);
            context.SaveChanges();
            return model;
        }
        public void Update(int id, RessourceModel model)
        {
            var existingRessource = context.Ressource.FirstOrDefault(r => r.Id == id);
            if (existingRessource != null)
            {
                existingRessource.Titre = model.Titre;
                existingRessource.IdCategory = model.IdCategory;
                existingRessource.IdUser = model.IdUser;
                context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var model = context.Ressource.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return;
            context.Ressource.Remove(model);
            context.SaveChanges();
        }

        public IEnumerable<RessourceModel> GetRessourcesByNameAndCategory(string searchText, int categoryId)
        {
            IQueryable<RessourceModel> query = context.Ressource;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(r => r.Titre.Contains(searchText));
            }

            if (categoryId != 0)
            {
                query = query.Where(r => r.IdCategory == categoryId);
            }

            return query.ToList();
        }
    }
}
