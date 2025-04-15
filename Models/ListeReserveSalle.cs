using System;

namespace GestionCinema.Models
{
    public class ListeReserveSalle
    {
        public DateTime? JourReservation { get; set; } // Correspond au type `date` en MariaDB
        public string SalleId { get; set; }           // Correspond à `varchar(20)`
        public string NomSalle { get; set; }          // Correspond à `varchar(50)`
        public double? SommeTotale { get; set; }      // Correspond à `double`
        public long NombreReservations { get; set; }  // Correspond à `bigint(21)`
    }
}
