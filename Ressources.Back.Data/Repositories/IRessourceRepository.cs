using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ressources.Back.Data.Models;

namespace Ressources.Back.Data.Repositories
{
    public interface IRessourceRepository
    {
        RessourceModel Create(RessourceModel Ressource);
        IEnumerable<RessourceModel> Read();
        RessourceModel GetRessourceById(int id);
        void Update(int id, RessourceModel Ressource);
        void Delete(int id);
    }
}
