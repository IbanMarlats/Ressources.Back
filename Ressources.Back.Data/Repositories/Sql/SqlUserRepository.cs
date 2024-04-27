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
        public UserModel Authenticate(string login, string mdp)
        {
            var user = context.User.FirstOrDefault(c => c.Login == login && c.Mdp == mdp);
            return user;
        }
    }
}
