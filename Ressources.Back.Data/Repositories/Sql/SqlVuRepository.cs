using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ressources.Back.Data.Models;

namespace Ressources.Back.Data.Repositories.Sql
{
    public class SqlVuRepository : IVuRepository
    {
        private readonly DataContext context;
        public SqlVuRepository(DataContext context)
        {
            this.context = context;
        }
        public IEnumerable<VuModel> Read()
        {
            return context.Vu.ToList();
        }
        public VuModel GetVuById(int id)
        {
            VuModel? vu = context.Vu.FirstOrDefault(c => c.Id == id);

            if (vu != null)
            {
                // Accès sécurisé aux propriétés de commentaire car commentaire n'est pas null
                Console.WriteLine($"La vu est trouvé");
            }
            else
            {
                // Gérer le cas où aucun commentaire n'est trouvé avec l'identifiant spécifié
                Console.WriteLine("Aucune vu trouvée avec cet identifiant.");
            }

            return vu;

        }
        public VuModel Create(VuModel model)
        {
            context.Vu.Add(model);
            context.SaveChanges();
            return model;
        }
        public void Update(int id, VuModel model)
        {
            context.Vu.Update(model);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var model = context.Vu.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return;
            context.Vu.Remove(model);
            context.SaveChanges();
        }
    }
}
