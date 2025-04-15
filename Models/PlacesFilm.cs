namespace GestionCinema.Models
{
    public class PlacesFilm
    {
        public string IdDiffusion { get; set; }
        public string FilmTitre { get; set; }
        public string NomSalle { get; set; }
        public string NomColonne { get; set; }
        public int NbLignes { get; set; }
        public int NbPlaces { get; set; }
        public string NumPlace { get; set; }
        public string IdReservation { get; set; }
        public int EtatPlace { get; set; }
        public DateTime DateReservation { get; set; }

    }

}