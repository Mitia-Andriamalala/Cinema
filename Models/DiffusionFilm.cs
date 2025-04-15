namespace GestionCinema.Models
{
    public class DiffusionFilm
    {
        public string IdDiffusion { get; set; }
        public string SalleId { get; set; }
        public string NomSalle { get; set; }
        public string FilmId { get; set; }
        
        public DateTime PlageHoraireDebut { get; set; }
        public DateTime PlageHoraireFin { get; set; }
        public string Titre { get; set; }
        public string CategorieId { get; set; }
        public TimeSpan Duree { get; set; }
        public byte[] Image { get; set; }
        public string ClassificationId { get; set; }
        public string Categorie { get; set; }
        public string Classification { get; set; }
    }
}