using System;
using System.Collections.Generic;
using System.Linq;
using GestionCinema.Models;
using GestionCinema.Services;
using Microsoft.AspNetCore.Mvc;


namespace Cinema.Services.FonctionPage
{
    public class ClientPage
    {
        
        public string connexion(String name)
        {
            try
            {
                Console.WriteLine("nom= "+name);
                FonctionDAO<User> fUser = new FonctionDAO<User>();
                string req = "name='" + name + "'";
                var util = fUser.MakaDonnerReq("user", req, new User());

                if (util.Count > 0)
                {
                    return util[0].idUser;
                }
                else
                {
                    throw new Exception("Aucun compte trouvé");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<ListeReserve> reservationPers(string idOlona)
        {
            FonctionDAO<ListeReserve> fonctionGet = new FonctionDAO<ListeReserve>();
            string req="idUser='"+idOlona+"'";
            var reservationPerso = fonctionGet.MakaDonnerReq("ListeReserve", req,new ListeReserve());
            return reservationPerso;
        }


        public List<DiffusionFilm> lsDiffusionFilm()
        {
            FonctionDAO<DiffusionFilm> fonctionGet = new FonctionDAO<DiffusionFilm>();
            var DiffusionFilmData = fonctionGet.MakaDonnerRehetra("DiffusionFilm", new DiffusionFilm());
            return DiffusionFilmData;
        }

        public List<ModePaiement> lsModePaiement()
        {
            FonctionDAO<ModePaiement> fonctionGet = new FonctionDAO<ModePaiement>();
            var modePaiement = fonctionGet.MakaDonnerRehetra("ModePaiement", new ModePaiement());
            return modePaiement;
        }

        public void doReservation(string idDiffusion, string place, int choix,string userId,string mode,DateTime dtRes,double vola)
        {
            try
            {
                // var dtRes = DateTime.Now;
                Console.WriteLine("Date actuelle: "+dtRes);
                string requete = "diffusionId='" + idDiffusion + "' and placeId='" + place + "'";
                FonctionDAO<Reservation> fonctionDAOReservationGet = new FonctionDAO<Reservation>();
                var verifExiste = fonctionDAOReservationGet.MakaDonnerReq("Reservation", requete, new Reservation());

                if (verifExiste.Count > 0)
                {
                    Console.Error.WriteLine("Cette place a déjà une réservation");
                    throw new Exception("Cette place " + place + " a déjà une réservation");
                }
                else
                {
                    Reservation reservation = new Reservation(place,choix,idDiffusion,userId,dtRes);
                    FonctionDAO<Reservation> fonctionDAOReservation = new FonctionDAO<Reservation>();
                    fonctionDAOReservation.Insertion("Reservation", reservation);

                    string requeteLast = "diffusionId='" + idDiffusion + "' and placeId='" + place + "' and userId='"+userId+"'";
                    FonctionDAO<Reservation> fonctionDAOReservationGetId = new FonctionDAO<Reservation>();
                    var lastId = fonctionDAOReservationGetId.MakaDonnerReq("Reservation", requeteLast, new Reservation());
                    string idRes=lastId[0].IdReservation;

                    Paiement paiement =new Paiement(mode,idRes,vola);
                    FonctionDAO<Paiement> fonctionDAOPaiement = new FonctionDAO<Paiement>();
                    fonctionDAOPaiement.Insertion("Paiement", paiement);


                }
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public void maj(string reserveId)
        {
            string req = $"idReservation = '{reserveId}'";
            FonctionDAO<Reservation> res = new FonctionDAO<Reservation>();
            Reservation reserve = new Reservation { EtatPlace = 1 };
            res.maj(reserve, "reservation", req);

            Console.WriteLine("Reservation = " + reserveId);
        }

        public void supprimer(string reserveId)
        {
            string req = $"idReservation = '{reserveId}'";
            FonctionDAO<Reservation> res = new FonctionDAO<Reservation>();
            res.suppression(new Reservation(), "reservation", req);

            Console.WriteLine("Reservation = " + reserveId);
        }

    }   
}
