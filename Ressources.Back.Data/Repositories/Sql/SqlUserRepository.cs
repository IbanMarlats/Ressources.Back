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
        

        public UserModel GetUserByLogin(string login)
        {
            UserModel? user = context.User.FirstOrDefault(c => c.Login == login );

            return user;

        }
        public UserModel Create(UserModel model)
        {
            
            if (GetUserByLogin(model.Login) != null)
            {
                throw new InvalidOperationException("Username already exists");
            }
            context.User.Add(model);
            context.SaveChanges();
            return model;
        }
        public void Update(int id, UserModel model)
        {
            var existingUser = context.User.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                existingUser.Login = model.Login;
                existingUser.Mdp = model.Mdp;
                existingUser.Activate = model.Activate;
                existingUser.Age = model.Age;
                existingUser.SituationFamiliale = model.SituationFamiliale;
                existingUser.CSP = model.CSP;
                existingUser.Loisir = model.Loisir;
                existingUser.Autre = model.Autre;
                existingUser.IdTypeUser = model.IdTypeUser;
                context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var model = context.User.FirstOrDefault(x => x.Id == id);
            if (model == null)
                return;
            context.User.Remove(model);
            context.SaveChanges();
        }
        public UserModel Authenticate(string login, string mdp)
        {
            var user = context.User.FirstOrDefault(c => c.Login == login && c.Mdp == mdp);
            return user;
        }
    }
}
