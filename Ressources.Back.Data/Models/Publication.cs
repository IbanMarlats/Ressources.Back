namespace Ressources.Back.Data.Models
{
    public class PublicationModel
    {
        public int Id { get; set; }
        public string Titre { get; set; } = "";
        public string Contenu { get; set; } = "";
        public int NbLike { get; set; }
        public int IdRessource { get; set; }
    }
}
