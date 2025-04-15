using System;
using System.Collections.Generic;
using System.Linq;

namespace GestionCinema.Services
{
    public class FonctionDAO<O> : CRUD where O : new()
    {
        public FonctionDAO() {}

        // Insertion d'un objet dans la table spécifiée
        public void Insertion(string table, object objetInserer)
        {
            try
            {
                this.SetNomTable(table);
                this.InsererObjet(objetInserer);
                Console.WriteLine($"Succès de l'insertion dans {table}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Échec de l'insertion : " + e.Message);
                Console.Error.WriteLine(e.Message);
            }
        }

        // Récupération de toutes les données de la table
        public List<O> MakaDonnerRehetra(string table, O classe)
        {
            this.SetNomTable(table);
            List<O> resultaList = null;

            try
            {
                // Récupérer toutes les données de la table en utilisant la méthode générique GetAllDataObject
                resultaList = this.GetAllDataObject(classe); // Ensure that GetAllDataObject is used with proper type

                // Vérifier si la liste est vide ou null
                if (resultaList == null || !resultaList.Any())
                {
                    throw new Exception($"Aucune donnée trouvée dans {table}");
                }
                else
                {
                    Console.WriteLine($"Récupération réussie des données dans {table}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur de récupération des données : " + e.Message);
                Console.Error.WriteLine(e.Message);
            }
            return resultaList;
        }

        // Récupération de toutes les données de la table
        public List<O> MakaDonnerReq(string table,string requete ,O classe)
        {
            this.SetNomTable(table);
            List<O> resultaList = null;

            try
            {
                // Récupérer toutes les données de la table en utilisant la méthode générique GetAllDataObject
                resultaList = this.SelectObjetRequete(classe,requete); // Ensure that GetAllDataObject is used with proper type

                // Vérifier si la liste est vide ou null
                if (resultaList == null || !resultaList.Any())
                {
                    throw new Exception($"Aucune donnée trouvée dans {table}");
                }
                else
                {
                    Console.WriteLine($"Récupération réussie des données dans {table}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur de récupération des données : " + e.Message);
                Console.Error.WriteLine(e.Message);
            }
            return resultaList;
        }

        public void suppression(O classe,string table,string requete)
        {
            try
            {
                this.SetNomTable(table);
                this.Delete(classe,requete);
                Console.WriteLine("SUPPRESSION REUSSITE");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                throw;
            }
        }


        public void maj(O classe,string table,string requete)
        {
            try
            {
                this.SetNomTable(table);
                this.UpdateObjet(classe,requete);
                Console.WriteLine("UPDATE REUSSITE");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                throw;
            }
        }
    }   
}
