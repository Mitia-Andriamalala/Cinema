using System;
using System.Collections.Generic;
using System.Linq;
using GestionCinema.Models;
using GestionCinema.Services;


namespace Cinema.Services.FonctionPage
{
    public class Administrateur
    {
        public List<InfoSalle> getListeSalle()
        {
            FonctionDAO<InfoSalle> fonctionGetInfoSalle = new FonctionDAO<InfoSalle>();
            var listeInfoSalle = fonctionGetInfoSalle.MakaDonnerRehetra("infoSalle", new InfoSalle());
            return listeInfoSalle;
        }

        public List<ListeReserve> getListeReservation()
        {
            FonctionDAO<ListeReserve> fonctionGetListeReserve = new FonctionDAO<ListeReserve>();
            var listeListeReserve = fonctionGetListeReserve.MakaDonnerRehetra("ListeReserve", new ListeReserve());
            return listeListeReserve;
        }

        public List<ListeReserve> getListeReservationSalleInfo(string salle)
        {
            string requete="group by dateReservation where salleId='"+salle+"'";
            FonctionDAO<ListeReserve> fonctionGetListeReserve = new FonctionDAO<ListeReserve>();
            var listeListeReserve = fonctionGetListeReserve.MakaDonnerReq("ListeReserve", requete ,new ListeReserve());
            return listeListeReserve;
        }

        public List<ListeReserveSalle> getListeReserveSalle()
        {
            FonctionDAO<ListeReserveSalle> fonctionGetListeReserveSalle = new FonctionDAO<ListeReserveSalle>();
            var listeListeReserveSalle = fonctionGetListeReserveSalle.MakaDonnerRehetra("ListeReserveSalle", new ListeReserveSalle());
            return listeListeReserveSalle;
        }
        
    }   
}
