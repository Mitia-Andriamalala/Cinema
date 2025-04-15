using Microsoft.AspNetCore.Mvc;
using GestionCinema.Models;
using GestionCinema.Services;
using Newtonsoft.Json;


namespace Cinema.Controllers
{
    public class DiffusionController : Controller
    {

        [HttpGet]
        public IActionResult diffusion()
        {
            FonctionDAO<Salle> fonctionGetSalle = new FonctionDAO<Salle>();
            var listeSalle = fonctionGetSalle.MakaDonnerRehetra("Salle", new Salle());

            FonctionDAO<Film> fonctionGetFilm = new FonctionDAO<Film>();
            var listeFilm = fonctionGetFilm.MakaDonnerRehetra("Film", new Film());

            ViewBag.SalleList = listeSalle;
            ViewBag.FilmList = listeFilm;

            return View();
        }


        [HttpGet]
        public IActionResult ListeDiffusion()
        {
            FonctionDAO<DiffusionFilm> fonctionGetDiffusionFilm = new FonctionDAO<DiffusionFilm>();
            var listeDiffusionFilm = fonctionGetDiffusionFilm.MakaDonnerRehetra("DiffusionFilm", new DiffusionFilm());
            return View(listeDiffusionFilm);
        }
        

        [HttpPost]
        public IActionResult saveDiffusion(string salle, string film, DateTime plageDebt)
        {
            try
            {
                // Vérification des heures d'ouverture et de fermeture de la salle
                FonctionDAO<Salle> fonctionGetSalle = new FonctionDAO<Salle>();
                string requeteSalle = "idSalle='" + salle + "'";
                var listeSalleReq = fonctionGetSalle.MakaDonnerReq("salle", requeteSalle, new Salle());

                if (listeSalleReq == null || listeSalleReq.Count == 0)
                {
                    throw new Exception("Salle introuvable.");
                }

                // Récupérer les heures d'ouverture et de fermeture
                var salleInfo = listeSalleReq.First();
                TimeSpan ouverture = salleInfo.ouverture;
                TimeSpan fermeture = salleInfo.fermeture;

                // Vérifier si plageDebt est en dehors des heures autorisées
                TimeSpan plageDebtTime = plageDebt.TimeOfDay; // Extraire la partie heure
                if (plageDebtTime < ouverture || plageDebtTime > fermeture)
                {
                    throw new Exception("Hors plage des heures de la salle qui est de "+ouverture+" -> "+fermeture+" mais le votre "+plageDebtTime);
                }

                else
                {
                    // Récupération de la durée du film
                    FonctionDAO<Film> fonctionGetFilm = new FonctionDAO<Film>();
                    string requeteFilm = "idFilm='" + film + "'";
                    var listeFilmReq = fonctionGetFilm.MakaDonnerReq("film", requeteFilm, new Film());

                    TimeSpan? duration = listeFilmReq.FirstOrDefault()?.Duree;
                    if (duration == null)
                    {
                        throw new Exception("Durée du film introuvable.");
                    }

                    // Calcul de la plage finale
                    DateTime plageFinal = plageDebt.Add(duration.Value);

                    // Vérification des conflits de diffusion
                    FonctionDAO<Diffusion> fonctionGetDiffusion = new FonctionDAO<Diffusion>();
                    string requeteDiff = $@"
                        (filmId = '{film}' AND plageHoraireDebut = '{plageDebt:yyyy-MM-dd HH:mm:ss}') 
                        OR (filmId = '{film}' AND plageHoraireDebut < '{plageFinal:yyyy-MM-dd HH:mm:ss}' AND plageHoraireFin > '{plageDebt:yyyy-MM-dd HH:mm:ss}')";

                    var listeFilmReqDif = fonctionGetDiffusion.MakaDonnerReq("Diffusion", requeteDiff, new Diffusion());

                    if (listeFilmReqDif != null && listeFilmReqDif.Count > 0)
                    {
                        throw new Exception("Un conflit existe pour cette plage horaire.");
                    }

                    // Insérer la nouvelle diffusion
                    Diffusion diffusion = new Diffusion(salle, film, plageDebt, plageFinal);

                    FonctionDAO<Diffusion> fonction = new FonctionDAO<Diffusion>();
                    fonction.Insertion("diffusion", diffusion);

                    return RedirectToAction("ListeDiffusion");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("diffusion");
            }
        }


        public IActionResult PlaceInfo(string salleId, string idDiff)
        {
            // Récupération des informations de la salle de places
            FonctionDAO<PlacesDispo> fonctionDAOPlacesDispo = new FonctionDAO<PlacesDispo>();
            var placesDispo = fonctionDAOPlacesDispo.MakaDonnerReq("PlacesDispo", $"salleId='{salleId}'", new PlacesDispo());

            // Récupération des places spécifiques à la diffusion par idDiffusion
            FonctionDAO<Reservation> fonctionDAOPlacesFilm = new FonctionDAO<Reservation>();
            var placesFilm = fonctionDAOPlacesFilm.MakaDonnerReq("Reservation", $"diffusionId='{idDiff}'", new Reservation());

            // Transmettre les places et les informations de diffusion à la vue
            ViewBag.Places = placesDispo;
            ViewBag.PlacesFilm = placesFilm;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReservationStatus()
        {
            using (var reader = new System.IO.StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync(); // Read the request body
                var data = JsonConvert.DeserializeObject<dynamic>(body); // Deserialize to a dynamic object
                string idReservation = data.idReservation; // Retrieve the reservation ID
                
                try
                {
                    // Perform the deletion operation
                    FonctionDAO<Reservation> fRes = new FonctionDAO<Reservation>();
                    string requete = "idReservation='" + idReservation + "'";
                    fRes.suppression(new Reservation(), "reservation", requete);

                    // Return a success response
                    return Json(new { success = true, message = "Suppression réussie pour la réservation: " + idReservation });
                }
                catch (Exception ex)
                {
                    // Handle any errors and return an error message
                    return Json(new { success = false, message = "Erreur lors de la suppression: " + ex.Message });
                }
            }
        }

    }
}
