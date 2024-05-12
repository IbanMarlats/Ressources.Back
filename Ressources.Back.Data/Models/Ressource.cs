namespace Ressources.Back.Data.Models
{
    public class RessourceModel
    {
        public int Id { get; set; }
        public string Titre { get; set; } = "";
        public int IdCategory { get; set; }
        public int IdUser { get; set; }
    }
}
