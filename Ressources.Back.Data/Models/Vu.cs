using System.ComponentModel.DataAnnotations;

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
