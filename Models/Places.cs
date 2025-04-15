namespace GestionCinema.Models
{
public class Places
{
    public string SallePlaceId { get; set; }
    public string Colonne { get; set; }
    
    public string NumPlace { get; set; }

    // Constructor
    public Places(string sallePlaceId,string nColonne)
    {
        SallePlaceId = sallePlaceId;
        Colonne=nColonne;
    }

    public Places()
    {
    }
}
}