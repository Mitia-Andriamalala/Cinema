namespace GestionCinema.Models
{
    public class Film
    {
        public string IdFilm { get; set; } // Correspond à VARCHAR(20)
        public string Titre { get; set; } // Correspond à VARCHAR(150)
        public byte[] Image { get; set; } // Correspond à BLOB
        public string CategorieId { get; set; } // Correspond à VARCHAR(20)
        public TimeSpan Duree { get; set; } // Correspond à TIME
        public string ClassificationId { get; set; } // Correspond à VARCHAR(20)

        // Constructor
        public Film(string titre, string categorieId, TimeSpan duree, string classificationId,byte[] sary)
        {
            Titre = titre;
            CategorieId = categorieId;
            Duree = duree;
            ClassificationId = classificationId;
            Image=sary;
        }
        public Film(){}
    }
}