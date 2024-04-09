using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ressources.Back.Data.Models;

namespace Ressources.Back.Data.Repositories
{
    public interface IVuRepository
    {
        VuModel Create(VuModel Vu);
        IEnumerable<VuModel> Read();
        void Update(int id, VuModel Vu);
        void Delete(int idUser, int idRessource);
    }
}
