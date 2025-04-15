using Microsoft.AspNetCore.Mvc;
using GestionCinema.Models;
using GestionCinema.Services;

namespace Cinema.Controllers
{
    public class AdminController : Controller
    {

        [HttpGet]
        public IActionResult AccueilAdmin()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Gestion()
        {
            FonctionDAO<InfoSalle> fonctionGetInfoSalle = new FonctionDAO<InfoSalle>();
            var listeInfoSalle = fonctionGetInfoSalle.MakaDonnerRehetra("infoSalle", new InfoSalle());
            return View(listeInfoSalle);
        }

        [HttpGet]
        public IActionResult Reservation()
        {
            FonctionDAO<ListeReserve> fonctionGetListeReserve = new FonctionDAO<ListeReserve>();
            var listeListeReserve = fonctionGetListeReserve.MakaDonnerRehetra("ListeReserve", new ListeReserve());
            return View(listeListeReserve);
        }

        
    }
}
