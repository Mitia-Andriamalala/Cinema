using System;
using System.Collections.Generic;
using System.Linq;
using GestionCinema.Models;
using GestionCinema.Services;


namespace Cinema.Services.FonctionPage
{
    public class GestionPage
    {
        public void setSalle(string salle,TimeSpan ouvre,TimeSpan ferme)
        {
            Salle salle1 = new Salle(salle,ouvre,ferme);
            FonctionDAO<Salle> fonctionSalle = new FonctionDAO<Salle>();
            fonctionSalle.Insertion("salle", salle1);
        }


        public void setPlaceSalle(string salle,string col,int nbLine,int nbPl)
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
        }

    }   
}
