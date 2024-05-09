using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ressources.Back.Data.Models;

namespace Ressources.Back.Data.Repositories
{
    public interface IPublicationRepository
    {
        PublicationModel Create(PublicationModel Publication);
        IEnumerable<PublicationModel> Read();
        PublicationModel GetPublicationById(int id);
        void Update(int id, PublicationModel Publication);
        void Delete(int id);
        IEnumerable<PublicationModel> GetPublicationsByRessourceId(int ressourceId);
    }
}