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
        public void Delete(int idUser, int idRessource)
        {
            var model = context.Vu.FirstOrDefault(x => x.IdUser == idUser && x.IdRessource == idRessource);
            if (model == null)
                return;
            context.Vu.Remove(model);
            context.SaveChanges();
        }
    }
}
