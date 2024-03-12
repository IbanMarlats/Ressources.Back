using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ressources.Back.Data.Models;

namespace Ressources.Back.Data.Repositories
{
    public interface ITypeUserRepository
    {
        TypeUserModel Create(TypeUserModel typeUser);
        IEnumerable<TypeUserModel> Read();
        TypeUserModel GetTypeUserById(int id);
        void Update(int id, TypeUserModel typeUser);
        void Delete(int id);
    }
}
