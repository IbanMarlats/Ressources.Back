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

            return typeUser ?? throw new InvalidOperationException($"TypeUser with id {id} not found");

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
