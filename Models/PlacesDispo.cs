namespace GestionCinema.Models
{
    public class PlacesDispo
    {
        public string IdSallePlaces { get; set; }  // Id de la place dans la salle
        public string SalleId { get; set; }        // Id de la salle
        public string NomColonne { get; set; }     // Nom de la colonne de la salle
        public int NbLignes { get; set; }          // Nombre de lignes dans la salle
        public int NbPlaces { get; set; }          // Nombre total de places dans la salle
        public string NumPlace { get; set; }  
    }
}