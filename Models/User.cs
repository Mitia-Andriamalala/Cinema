namespace GestionCinema.Models
{
    public class User
    {
        public string idUser { get; set; }
        public string name { get; set; }

        // Constructeur sans paramètres requis pour le générique
        public User() {}
        
        // Constructeur avec paramètres si nécessaire
        public User(string nom)
        {
            name = nom;
        }
    }
}
