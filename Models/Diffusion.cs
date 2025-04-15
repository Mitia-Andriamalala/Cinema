namespace GestionCinema.Models
{
    public class Diffusion
    {
        public string IdDiffusion { get; set; }
        public string SalleId { get; set; }
        public string FilmId { get; set; }
        public DateTime PlageHoraireDebut { get; set; }
        public DateTime PlageHoraireFin { get; set; }

        // Constructor
        public Diffusion(string salleId, string filmId, DateTime plageHoraireDebut, DateTime plageHoraireFin)
        {
            SalleId = salleId;
            FilmId = filmId;
            PlageHoraireDebut = plageHoraireDebut;
            PlageHoraireFin = plageHoraireFin;
        }
        public Diffusion(){}
    }
}