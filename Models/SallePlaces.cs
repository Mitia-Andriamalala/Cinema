namespace GestionCinema.Models
{
public class SallePlaces
{
    public string IdSallePlaces { get; set; }
    public string SalleId { get; set; }
    public string NomColonne { get; set; }
    public int NbLignes { get; set; }
    public int NbPlaces { get; set; }

    // Constructor
    public SallePlaces(string salleId, string nomColonne, int nbLignes, int nbPlaces)
    {
        SalleId = salleId;
        NomColonne = nomColonne;
        NbLignes = nbLignes;
        NbPlaces = nbPlaces;
    }
    public SallePlaces(){}
}
}