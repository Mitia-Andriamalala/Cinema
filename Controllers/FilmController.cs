using Microsoft.AspNetCore.Mvc;
using GestionCinema.Models;
using GestionCinema.Services;

namespace Cinema.Controllers
{
    public class FilmController : Controller
    {

        [HttpGet]
        public IActionResult InsertFilm()
        {
            // Récupérer la liste des catégories
            FonctionDAO<Categorie> fonctionGetCategorie = new FonctionDAO<Categorie>();
            var listeCategorie = fonctionGetCategorie.MakaDonnerRehetra("Categorie", new Categorie());

            // Récupérer la liste des classifications
            FonctionDAO<Classification> fonctionGetClassification = new FonctionDAO<Classification>();
            var listeClassification = fonctionGetClassification.MakaDonnerRehetra("classification", new Classification());

            // Utiliser ViewBag pour envoyer les données
            ViewBag.CategorieList = listeCategorie;
            ViewBag.ClassificationList = listeClassification;

            return View(); // Passer directement à la vue
        }

        [HttpGet]
        public IActionResult Film()
        {
            FonctionDAO<InfoFilm> fonctionGet = new FonctionDAO<InfoFilm>();
            var listeFilm = fonctionGet.MakaDonnerRehetra("infofilm", new InfoFilm());
            return View(listeFilm);
        }

        public IActionResult SaveFilm(string titre,string categorie,string classe,string duree,IFormFile image)
        {
            byte[] imageBytes = null;
            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                imageBytes = ms.ToArray();   
            }
            TimeSpan parsedDuree = ConvertStringToTimeSpan(duree);
            Film film=new Film(titre,categorie,parsedDuree,classe,imageBytes);
            FonctionDAO<Film> fonction = new FonctionDAO<Film>();
            fonction.Insertion("Film", film);

            return RedirectToAction("Film");
        }
        
        public TimeSpan ConvertStringToTimeSpan(string duree)
        {
            // Tente de convertir la chaîne duree en TimeSpan
            if (TimeSpan.TryParse(duree, out TimeSpan result))
            {
                // Si la conversion réussit, retourne le TimeSpan
                return result;
            }
            else
            {
                // Si la conversion échoue, lève une exception ou retourne un TimeSpan par défaut
                throw new FormatException("Le format de la durée est invalide.");
            }
        }


        [HttpGet]
        public IActionResult Categorie()
        {
            // Récupérer la liste des catégories
            FonctionDAO<Categorie> fonctionGetCategorie = new FonctionDAO<Categorie>();
            var listeCategorie = fonctionGetCategorie.MakaDonnerRehetra("Categorie", new Categorie());


            return View(listeCategorie); // Passer directement à la vue
        }

        public IActionResult SaveCategorie(string categorie)
        {
            Categorie categorie1=new Categorie(categorie);
            FonctionDAO<Categorie> fonction = new FonctionDAO<Categorie>();
            fonction.Insertion("categorie", categorie1);

            // Optionnel : Rediriger vers la page d'accueil après avoir enregistré la salle
            return RedirectToAction("Categorie");
        }


        [HttpGet]
        public IActionResult Classe()
        {
            // Récupérer la liste des catégories
            FonctionDAO<Classification> fonctionGetCategorie = new FonctionDAO<Classification>();
            var listeClasse = fonctionGetCategorie.MakaDonnerRehetra("Classification", new Classification());


            return View(listeClasse); // Passer directement à la vue
        }


        public IActionResult SaveClasse(string classe)
        {
            Classification classification=new Classification(classe);
            FonctionDAO<Classification> fonction = new FonctionDAO<Classification>();
            fonction.Insertion("classification", classification);

            // Optionnel : Rediriger vers la page d'accueil après avoir enregistré la salle
            return RedirectToAction("Classe");
        }

    }
}
