using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ressources.Back.Data.Models
{
    public class VuModel
    {
        [Key]
        public int IdUser { get; set; }
        [Key]
        public int IdRessource { get; set; }
        public DateOnly Date { get; set; }
    }
}
