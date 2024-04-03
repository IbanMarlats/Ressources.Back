using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ressources.Back.Data.Models
{
    public class RessourceModel
    {
        public int Id { get; set; }
        public string Titre { get; set; } = "";
        public string Contenu { get; set; } = "";
        public DateOnly DatePublication { get; set; }
        public int NbLike { get; set; }
        public int NbComment { get; set; }
        public int NbVu { get; set; }
    }
}
