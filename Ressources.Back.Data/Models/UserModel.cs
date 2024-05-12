namespace Ressources.Back.Data.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; } = "";
        public string Mdp { get; set; } = "";

        public int Activate { get; set; }
        public int Age { get; set; }
        public string SituationFamiliale { get; set; } = "";

        public string CSP { get; set; } = "";
        public string Loisir { get; set; } = "";
        public string Autre {  get; set; } = "";
        public int IdTypeUser { get; set; } = 1;
        public int IdStatus { get; set; }
    }
}
