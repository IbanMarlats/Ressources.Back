using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ressources.Back.Data.Models
{
    public class VuModel
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdRessource { get; set; }
        public DateOnly Date { get; set; }
    }
}
