namespace GestionCinema.Models
{
    public class Paiement
    {
        public string idPaiement { get; set; }
        public string modeId { get; set; }
        public string reservationId { get; set; }
        public double vola { get; set; }

        // Constructor
        public Paiement(string mode,string reservation,double monnaie)
        {
            modeId = mode;
            reservationId = reservation;
            vola = monnaie;
        }

        public Paiement(){}
    }
}
