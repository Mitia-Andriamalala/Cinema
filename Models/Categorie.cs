namespace GestionCinema.Models
{
    public class Categorie
    {
        public string IdCategorie { get; set; }
        public string categorie { get; set; }

        // Constructor
        public Categorie(string categorieName)
        {
            categorie = categorieName;
        }

        public Categorie(){}
    }
}
