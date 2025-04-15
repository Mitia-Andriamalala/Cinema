using System;
using System.Collections.Generic;
using System.Linq;
using GestionCinema.Models;
using GestionCinema.Services;


namespace Cinema.Services.FonctionPage
{
    public class FilmPage
    {
        public List<Categorie> getListeCategorie()
        {
            FonctionDAO<Categorie> fonctionGetCategorie = new FonctionDAO<Categorie>();
            var listeCategorie = fonctionGetCategorie.MakaDonnerRehetra("Categorie", new Categorie());
            return listeCategorie;
        }

        public List<Classification> getClasseFilm()
        {
            FonctionDAO<Classification> fonctionGetClassification = new FonctionDAO<Classification>();
            var listeClassification = fonctionGetClassification.MakaDonnerRehetra("classification", new Classification());
            return listeClassification;
        }


        public List<InfoFilm> getInfoFilm()
        {
            FonctionDAO<InfoFilm> fonctionGet = new FonctionDAO<InfoFilm>();
            var listeFilm = fonctionGet.MakaDonnerRehetra("infofilm", new InfoFilm());
            return listeFilm;
        }

        public void setFilm(string titre,string categorie,string classe,string duree,IFormFile image)
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
        }

        public TimeSpan ConvertStringToTimeSpan(string duree)
        {
            if (TimeSpan.TryParse(duree, out TimeSpan result))
            {
                return result;
            }
            else
            {
                throw new FormatException("Le format de la durée est invalide.");
            }
        }

        public void setCategorie(string val)
        {
            Categorie categorie1=new Categorie(val);
            FonctionDAO<Categorie> fonction = new FonctionDAO<Categorie>();
            fonction.Insertion("categorie", categorie1);
        }

        public void setClasse(string value)
        {
            Classification classification=new Classification(value);
            FonctionDAO<Classification> fonction = new FonctionDAO<Classification>();
            fonction.Insertion("classification", classification);
        }
    }   
}
