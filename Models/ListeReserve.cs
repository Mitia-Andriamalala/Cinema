namespace GestionCinema.Models
{
    public class ListeReserve
    {
        public string IdUser { get; set; }
        public string Name { get; set; }
        public string IdReservation { get; set; }
        public int EtatPlace { get; set; }
        public string DiffusionId { get; set; }
        public DateTime PlageHoraireDebut { get; set; }
        public DateTime PlageHoraireFin { get; set; }
        public string FilmId { get; set; }
        public string SalleId { get; set; }
        public string Titre { get; set; }
        public string CategorieId { get; set; }
        public string NomSalle { get; set; }
    }
}
