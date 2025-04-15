using Microsoft.AspNetCore.Mvc;
using GestionCinema.Models;
using GestionCinema.Services;

namespace Cinema.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Connecter(string nom)
        {
            try
            {
                Console.WriteLine("nom= "+nom);
                FonctionDAO<User> fUser = new FonctionDAO<User>();
                string req = "name='" + nom + "'";
                var util = fUser.MakaDonnerReq("user", req, new User());

                if (util.Count > 0)
                {
                    HttpContext.Session.SetString("UserId", util[0].idUser);

                    return RedirectToAction("ListeProgramme");
                }
                else
                {
                    throw new Exception("Aucun compte trouvé");
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult ListeReservation()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "Vous devez être connecté pour voir vos réservations.";
                return RedirectToAction("Index");
            }

            FonctionDAO<ListeReserve> fonctionGet = new FonctionDAO<ListeReserve>();
            string req="idUser='"+userId+"'";
            var reservationPerso = fonctionGet.MakaDonnerReq("ListeReserve", req,new ListeReserve());

            return View(reservationPerso);
        }


        public IActionResult ListeProgramme()
        {
            FonctionDAO<DiffusionFilm> fonctionGet = new FonctionDAO<DiffusionFilm>();
            var DiffusionFilmData = fonctionGet.MakaDonnerRehetra("DiffusionFilm", new DiffusionFilm());
            return View(DiffusionFilmData);
        }

        public IActionResult ReservationPage(string salleId, string idDiff)
        {
            FonctionDAO<PlacesDispo> fonctionDAOPlacesDispo = new FonctionDAO<PlacesDispo>();
            var placesDispo = fonctionDAOPlacesDispo.MakaDonnerReq("PlacesDispo", $"salleId='{salleId}'", new PlacesDispo());

            FonctionDAO<Reservation> fonctionDAOPlacesFilm = new FonctionDAO<Reservation>();
            var placesFilm = fonctionDAOPlacesFilm.MakaDonnerReq("Reservation", $"diffusionId='{idDiff}'", new Reservation());

            ViewBag.IdDiffusion = idDiff;
            ViewBag.salleId = salleId;
            ViewBag.Places = placesDispo;
            ViewBag.PlacesFilm = placesFilm;

            return View();
        }


        [HttpPost]
        public IActionResult Reservation(string salleId, string idDiffusion, string place, int choix, DateTime dtRes)
        {
            try
            {
                Console.WriteLine("idDiffusion: " + idDiffusion);
                Console.WriteLine("place: " + place);
                Console.WriteLine("choix: " + choix);
                Console.WriteLine("dtRes: " + dtRes);
                var userId = HttpContext.Session.GetString("UserId");
                if (string.IsNullOrEmpty(userId))
                {
                    TempData["ErrorMessage"] = "Vous devez être connecté pour voir vos réservations.";
                    return RedirectToAction("Index");
                }
                else
                {
                    if (string.IsNullOrEmpty(place) || choix == null || dtRes == null)
                    {
                        throw new Exception("Veuillez compléter tous les champs requis.");
                    }

                    string requete = "diffusionId='" + idDiffusion + "' and placeId='" + place + "'";
                    FonctionDAO<Reservation> fonctionDAOReservationGet = new FonctionDAO<Reservation>();
                    var verifExiste = fonctionDAOReservationGet.MakaDonnerReq("Reservation", requete, new Reservation());

                    if (verifExiste.Count > 0)
                    {
                        Console.Error.WriteLine("Cette place a déjà une réservation");
                        throw new Exception("Cette place " + place + " a déjà une réservation");
                    }

                    Reservation reservation = new Reservation(place,choix,idDiffusion,userId,dtRes);
                    FonctionDAO<Reservation> fonctionDAOReservation = new FonctionDAO<Reservation>();
                    fonctionDAOReservation.Insertion("Reservation", reservation);

                    return RedirectToAction("ListeProgramme");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ReservationPage", new { salleId, idDiffusion });
            }
        }


        public IActionResult Update(string idReservation)
        {
            string req = $"idReservation = '{idReservation}'";
            FonctionDAO<Reservation> res = new FonctionDAO<Reservation>();
            Reservation reserve = new Reservation { EtatPlace = 1 };
            res.maj(reserve, "reservation", req);

            Console.WriteLine("Reservation = " + idReservation);
            return RedirectToAction("ListeReservation");
        }
    }
}
