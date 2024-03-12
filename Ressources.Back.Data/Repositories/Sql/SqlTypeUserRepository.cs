using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ressources.Back.Data.Models;

namespace Ressources.Back.Data.Repositories.Sql
{
    public class SqlTypeUserRepository : ITypeUserRepository
    {
        private readonly DataContext context;
        public SqlTypeUserRepository(DataContext context)
        {
            this.context = context;
        }
        public IEnumerable<TypeUserModel> Read()
        {
            return context.TypeUser.ToList();
        }
        public TypeUserModel GetTypeUserById(int id)
        {
            TypeUserModel? typeUser = context.TypeUser.FirstOrDefault(c => c.Id == id);

            if (typeUser != null)
            {
                // Accès sécurisé aux propriétés de commentaire car commentaire n'est pas null
                Console.WriteLine($"La typeUser est trouvé");
            }
            else
            {
                // Gérer le cas où aucun commentaire n'est trouvé avec l'identifiant spécifié
                Console.WriteLine("Aucun typeUser trouvé avec cet identifiant.");
            }

            return typeUser;

        }
        public TypeUserModel Create(TypeUserModel model)
        {
            context.TypeUser.Add(model);
            context.SaveChanges();
            return model;
        }
        public void Update(int id, TypeUserModel model)
        {
            context.TypeUser.Update(model);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var model = context.TypeUser.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return;
            context.TypeUser.Remove(model);
            context.SaveChanges();
        }
    }
}
