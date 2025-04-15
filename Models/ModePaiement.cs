namespace GestionCinema.Models
{
    public class ModePaiement
    {
        public string idMode { get; set; }
        public string type { get; set; }

        // Constructor
        public ModePaiement(string type)
        {
            type = type;
        }

        public ModePaiement(){}
    }
}
