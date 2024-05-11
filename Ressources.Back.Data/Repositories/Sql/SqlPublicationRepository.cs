using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ressources.Back.Data.Models;

namespace Ressources.Back.Data.Repositories.Sql
{
    public class SqlPublicationRepository : IPublicationRepository
    {
        private readonly DataContext context;
        public SqlPublicationRepository(DataContext context)
        {
            this.context = context;
        }
        public IEnumerable<PublicationModel> Read()
        {
            return context.Publication.ToList();
        }
        public PublicationModel GetPublicationById(int id)
        {
            PublicationModel? publication = context.Publication.FirstOrDefault(c => c.Id == id);

            return publication;

        }
        public PublicationModel Create(PublicationModel model)
        {
            context.Publication.Add(model);
            context.SaveChanges();
            return model;
        }
        public void Update(int id, PublicationModel model)
        {
            context.Publication.Update(model);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var model = context.Publication.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return;
            context.Publication.Remove(model);
            context.SaveChanges();
        }
        public IEnumerable<PublicationModel> GetPublicationsByRessourceId(int ressourceId)
        {
            return context.Publication.Where(p => p.IdRessource == ressourceId).ToList();
        }
    }
}
