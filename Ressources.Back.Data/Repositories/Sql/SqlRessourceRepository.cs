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

            if (ressource != null)
            {
                // Accès sécurisé aux propriétés de commentaire car commentaire n'est pas null
                Console.WriteLine($"La ressource est trouvé");
            }
            else
            {
                // Gérer le cas où aucun commentaire n'est trouvé avec l'identifiant spécifié
                Console.WriteLine("Aucune ressource trouvée avec cet identifiant.");
            }

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
            context.Ressource.Update(model);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var model = context.Ressource.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return;
            context.Ressource.Remove(model);
            context.SaveChanges();
        }
    }
}
