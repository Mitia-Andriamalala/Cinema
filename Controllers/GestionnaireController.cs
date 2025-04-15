using Microsoft.AspNetCore.Mvc;
using GestionCinema.Models;
using GestionCinema.Services;

namespace Cinema.Controllers
{
    public class GestionnaireController : Controller
    {
        // Afficher la page d'accueil
        [HttpGet]
        public IActionResult AccueilAdmin()
        {
            return View();
        }

        // Sauvegarder la salle
        [HttpPost]
        public IActionResult SaveSalle(string salle,TimeSpan ouvre,TimeSpan ferme)
        {
            // Créez un objet Salle avec le nom de la salle
            Salle salle1 = new Salle(salle,ouvre,ferme);

            // Appeler la fonction DAO pour insérer la salle dans la base de données
            FonctionDAO<Salle> fonctionSalle = new FonctionDAO<Salle>();
            fonctionSalle.Insertion("salle", salle1);

            // Rediriger vers la page de gestion après l'ajout de la salle
            return RedirectToAction("Gestion", "Admin");
        }

        [HttpGet]
        public IActionResult PlaceSalle()
        {
            FonctionDAO<Salle> fonction = new FonctionDAO<Salle>();
            var listeClasse = fonction.MakaDonnerRehetra("Salle", new Salle());
            return View("~/Views/Admin/PlaceSalle.cshtml", listeClasse);

        }


        [HttpPost]
        public IActionResult InsertPlaceSalle(string salle,string col,int nbLine,int nbPl)
        {
            SallePlaces sallePlaces=new SallePlaces(salle,col,nbLine,nbPl);
            FonctionDAO<SallePlaces> fonction = new FonctionDAO<SallePlaces>();
            fonction.Insertion("sallePlaces", sallePlaces);
            int i=0;
            while (i<nbPl)
            {
                Places places=new Places(salle,col);
                FonctionDAO<Places> fonctionPl = new FonctionDAO<Places>();
                fonctionPl.Insertion("places", places);
                i++;
            }
            return RedirectToAction("PlaceSalle");
        }

        
    }
}
