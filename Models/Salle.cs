namespace GestionCinema.Models
{
    public class Salle
    {
        public string idSalle { get; set; }
        public string nomSalle { get; set; }
        public TimeSpan ouverture { get; set; }
        public TimeSpan fermeture { get; set; }

        // Constructeur sans paramètres requis pour le générique
        public Salle() {}
        
        // Constructeur avec paramètres si nécessaire
        public Salle(string nom,TimeSpan o,TimeSpan f)
        {
            nomSalle = nom;
            ouverture=o;
            fermeture=f;
        }
    }

}
