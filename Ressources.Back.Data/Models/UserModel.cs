using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ressources.Back.Data.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; } = "";
        public string Mdp { get; set; } = "";

        public int Age { get; set; }
        public string SituationFamiliale { get; set; } = "";

        public string CSP { get; set; } = "";
        public string Loisir { get; set; } = "";
        public string Autre {  get; set; } = "";
        public int IdTypeUser { get; set; } = 1;
        public int IdStatus { get; set; }
        //public virtual UserModel TypeUser { get; set; }
    }
}
