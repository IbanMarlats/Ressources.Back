using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ressources.Back.Data.Models;

namespace Ressources.Back.Data.Repositories
{
    public interface IUserRepository
    {
        UserModel Create(UserModel User);
        IEnumerable<UserModel> Read();
        UserModel GetUserByLogin(string login);
        void Update(int id, UserModel User);
        void Delete(int id);
        UserModel Authenticate(string login, string mdp);
    }
}
