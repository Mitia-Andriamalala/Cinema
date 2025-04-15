namespace GestionCinema.Models
{
    public class InfoSalle
    {
        public string NomSalle { get; set; }
        public string NomColonne { get; set; }
        public int NbLignes { get; set; }
        public int NbPlaces { get; set; }
        public TimeSpan ouverture { get; set; }
        public TimeSpan fermeture { get; set; }
    }
}
