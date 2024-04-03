using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ressources.Back.Data.Models;

namespace Ressources.Back.Data.Repositories.Sql
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly DataContext context;
        public SqlUserRepository(DataContext context)
        {
            this.context = context;
        }
        public IEnumerable<UserModel> Read()
        {
            return context.User.ToList();
        }
        public UserModel GetUserById(int id)
        {
            UserModel? user = context.User.FirstOrDefault(c => c.Id == id);

            if (user != null)
            {
                // Accès sécurisé aux propriétés de commentaire car commentaire n'est pas null
                Console.WriteLine($"L'utilisateur' est trouvé");
            }
            else
            {
                // Gérer le cas où aucun commentaire n'est trouvé avec l'identifiant spécifié
                Console.WriteLine("Aucun user trouvé avec cet identifiant.");
            }

            return user;

        }
        public UserModel Create(UserModel model)
        {
            context.User.Add(model);
            context.SaveChanges();
            return model;
        }
        public void Update(int id, UserModel model)
        {
            context.User.Update(model);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var model = context.User.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return;
            context.User.Remove(model);
            context.SaveChanges();
        }
    }
}
