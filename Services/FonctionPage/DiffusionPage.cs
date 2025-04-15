using System;
using System.Collections.Generic;
using System.Linq;
using GestionCinema.Models;
using GestionCinema.Services;


namespace Cinema.Services.FonctionPage
{
    public class DiffusionPage
    {
        public List<Salle> getSalle()
        {
            FonctionDAO<Salle> fonctionGetSalle = new FonctionDAO<Salle>();
            var listeSalle = fonctionGetSalle.MakaDonnerRehetra("Salle", new Salle());
            return listeSalle;
        }

        public List<Film> getListeFilm()
        {
            FonctionDAO<Film> fonctionGetFilm = new FonctionDAO<Film>();
            var listeFilm = fonctionGetFilm.MakaDonnerRehetra("Film", new Film());
            return listeFilm;
        }

        public List<DiffusionFilm> getListeDiffFilm()
        {
            FonctionDAO<DiffusionFilm> fonctionGetDiffusionFilm = new FonctionDAO<DiffusionFilm>();
            var listeDiffusionFilm = fonctionGetDiffusionFilm.MakaDonnerRehetra("DiffusionFilm", new DiffusionFilm());
            return listeDiffusionFilm;
        }

        public void insertDiffusion(string salle, string film,double vola ,DateTime plageDebt)
        {
            try
            {
                var dtRes = DateTime.Now;
                // if (plageDebt<=dtRes)
                // {
                //     throw new Exception("La date de diffusion doit etre prochain");
                // }
                if (salle==null || film==null || plageDebt==null || vola==0)
                {
                    throw new Exception("Rempli tous les champs");
                }

                FonctionDAO<Salle> fonctionGetSalle = new FonctionDAO<Salle>();
                string requeteSalle = "idSalle='" + salle + "'";
                var listeSalleReq = fonctionGetSalle.MakaDonnerReq("salle", requeteSalle, new Salle());

                if (listeSalleReq == null || listeSalleReq.Count == 0)
                {
                    throw new Exception("Salle introuvable.");
                }

                var salleInfo = listeSalleReq.First();
                TimeSpan ouverture = salleInfo.ouverture;
                TimeSpan fermeture = salleInfo.fermeture;

                TimeSpan plageDebtTime = plageDebt.TimeOfDay;
                if (plageDebtTime < ouverture || plageDebtTime > fermeture)
                {
                    throw new Exception("Hors plage des heures de la salle qui est de "+ouverture+" -> "+fermeture+" mais le votre "+plageDebtTime);
                }

                else
                {
                    FonctionDAO<Film> fonctionGetFilm = new FonctionDAO<Film>();
                    string requeteFilm = "idFilm='" + film + "'";
                    var listeFilmReq = fonctionGetFilm.MakaDonnerReq("film", requeteFilm, new Film());

                    TimeSpan? duration = listeFilmReq.FirstOrDefault()?.Duree;
                    if (duration == null)
                    {
                        throw new Exception("Durée du film introuvable.");
                    }

                    DateTime plageFinal = plageDebt.Add(duration.Value);

                    FonctionDAO<Diffusion> fonctionGetDiffusion = new FonctionDAO<Diffusion>();
                    string requeteDiff = $@"
                        (salleId = '{salle}' AND plageHoraireDebut = '{plageDebt:yyyy-MM-dd HH:mm:ss}') 
                        OR (salleId = '{salle}' AND plageHoraireDebut < '{plageFinal:yyyy-MM-dd HH:mm:ss}' AND plageHoraireFin > '{plageDebt:yyyy-MM-dd HH:mm:ss}')";

                    var listeFilmReqDif = fonctionGetDiffusion.MakaDonnerReq("Diffusion", requeteDiff, new Diffusion());

                    if (listeFilmReqDif != null && listeFilmReqDif.Count > 0)
                    {
                        throw new Exception("Un conflit existe pour cette plage horaire pour salle "+salleInfo.nomSalle);
                    }

                    Diffusion diffusion = new Diffusion(salle, film, vola ,plageDebt, plageFinal);

                    FonctionDAO<Diffusion> fonction = new FonctionDAO<Diffusion>();
                    fonction.Insertion("diffusion", diffusion);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<PlacesDispo> getPlacesdispo(string salleId)
        {
            FonctionDAO<PlacesDispo> fonctionDAOPlacesDispo = new FonctionDAO<PlacesDispo>();
            var placesDispo = fonctionDAOPlacesDispo.MakaDonnerReq("PlacesDispo", $"salleId='{salleId}'", new PlacesDispo());
            return placesDispo;
        }

        public List<Reservation> getPlacesPrisReserverDiff(string idDiff)
        {
            FonctionDAO<Reservation> fonctionDAOPlacesFilm = new FonctionDAO<Reservation>();
            var placesFilm = fonctionDAOPlacesFilm.MakaDonnerReq("Reservation", $"diffusionId='{idDiff}'", new Reservation());
            return placesFilm;
        }


        public void updateReservationStatu(String reserveId)
        {
            FonctionDAO<Reservation> fRes = new FonctionDAO<Reservation>();
            string requete = "idReservation='" + reserveId + "'";
            fRes.suppression(new Reservation(), "reservation", requete);
        }
    }   
}
