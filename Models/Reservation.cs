namespace GestionCinema.Models
{
public class Reservation
{
    public string IdReservation { get; set; }
    public string PlaceId { get; set; }
    public int EtatPlace { get; set; }
    public string DiffusionId { get; set; }
    public string UserId { get; set; }
    public DateTime DateReservation { get; set; }

    // Constructor
    public Reservation(string placeId, int etatPlace, string diffusionId, string user,DateTime dateReservation)
    {
        PlaceId = placeId;
        EtatPlace = etatPlace;
        DiffusionId = diffusionId;
        UserId=user;
        DateReservation = dateReservation;
    }
    public Reservation(){}
}
}