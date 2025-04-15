using System;
using MySqlConnector;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using GestionCinema.Services.Utilitaire;

namespace GestionCinema.Services
{
    public class CRUD
    {
        protected string nomTable;

        // Constructor
        public CRUD() { }

        // Updated ManaoConnexion to use the Connexion class
        protected MySqlConnection ManaoConnexion()
        {
            // Reusing the existing connection logic in the Connexion class
            return new Connexion().GetConnexion();
        }

        // Get the name of the table
        protected string GetNomTable()
        {
            return nomTable;
        }

        // Set the table name (with validation)
        protected void SetNomTable(string table)
        {
            if (string.IsNullOrEmpty(table))
            {
                throw new Exception("Indiquer la table ou vous voulez travailler");
            }
            nomTable = table;
        }

        // Insert an object into the database
        protected void InsererObjet(object obj)
        {
            var objType = obj.GetType();
            // Use GetProperties instead of GetFields
            var properties = objType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var columns = string.Join(",", properties.Select(p => p.Name));
            var parameters = string.Join(",", properties.Select(p => "@" + p.Name));

            var query = $"INSERT INTO {GetNomTable()} ({columns}) VALUES ({parameters})";

            using (var conn = ManaoConnexion())
            {
                try
                {
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        foreach (var prop in properties)
                        {
                            cmd.Parameters.AddWithValue("@" + prop.Name, prop.GetValue(obj) ?? DBNull.Value);
                        }
                        cmd.ExecuteNonQuery();
                    }
                    Console.WriteLine($"REUSSITE INERT");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during insert: {ex.Message}");
                    throw;
                }
            }
        }

        public List<T> GetAllDataObject<T>(T objectTemplate) where T : new()
        {
            var objClass = objectTemplate.GetType();
            string requete = $"SELECT * FROM {GetNomTable()}";
            var resultats = new List<T>();

            using (var conn = ManaoConnexion())
            {
                try
                {
                    using (var cmd = new MySqlCommand(requete, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Utiliser GetProperties() au lieu de GetFields()
                        var properties = objClass.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                        while (reader.Read())
                        {
                            var objet = new T();
                            foreach (var prop in properties)
                            {
                                // Mapper les valeurs du reader aux propriétés de l'objet
                                var value = reader[prop.Name];
                                prop.SetValue(objet, value == DBNull.Value ? null : value);
                            }
                            resultats.Add(objet);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during data retrieval: {ex.Message}");
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return resultats;
        }


        protected List<T> SelectObjetRequete<T>(T objectTemplate, string whereClause) where T : new()
        {
            var result = new List<T>();
            var objType = objectTemplate.GetType();
            var query = $"SELECT * FROM {GetNomTable()}";

            // Ajouter la condition WHERE si elle est fournie
            if (!string.IsNullOrWhiteSpace(whereClause))
                query += $" WHERE {whereClause}";

            using (var conn = ManaoConnexion())
            {
                try
                {
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Obtenir les propriétés publiques de l'objet T
                        var properties = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                        while (reader.Read())
                        {
                            // Créer une instance de l'objet T
                            var obj = Activator.CreateInstance<T>();

                            foreach (var prop in properties)
                            {
                                // Mapper les valeurs du reader aux propriétés de l'objet
                                var value = reader[prop.Name];

                                // Vérifier si la valeur est DBNull et la remplacer par null
                                if (value == DBNull.Value)
                                {
                                    value = null;
                                }

                                // Assigner la valeur à la propriété correspondante
                                prop.SetValue(obj, value);
                            }

                            result.Add(obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during select with condition: {ex.Message}");
                    throw;
                }
                finally
                {
                    // Assurer la fermeture de la connexion si elle est toujours ouverte
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }

            return result;
        }


        // Select data with specific columns and conditions
        protected List<Dictionary<string, object>> SelectDataAvecConditionCalcul(string columns, string whereClause)
        {
            var result = new List<Dictionary<string, object>>();
            var query = $"SELECT {columns} FROM {GetNomTable()}";

            if (!string.IsNullOrWhiteSpace(whereClause))
                query += $" WHERE {whereClause}";

            using (var conn = ManaoConnexion())
            {
                try
                {
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.GetValue(i);
                            }
                            result.Add(row);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during select with condition and calculation: {ex.Message}");
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return result;
        }

        // Delete an object based on a condition
        public void Delete(object obj, string whereClause)
        {
            var query = $"DELETE FROM {GetNomTable()}";

            if (!string.IsNullOrWhiteSpace(whereClause))
                query += $" WHERE {whereClause}";

            using (var conn = ManaoConnexion())
            {
                try
                {
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during delete: {ex.Message}");
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }

        protected void UpdateObjet(object obj, string whereClause)
        {
            var objType = obj.GetType();
            var properties = objType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            // Construire les colonnes et paramètres pour l'UPDATE
            var setClauses = properties
                .Where(p => p.GetValue(obj) != null) // Inclure seulement les propriétés ayant une valeur définie
                .Select(p => $"{p.Name} = @{p.Name}")
                .ToList();

            if (!setClauses.Any())
            {
                throw new Exception("Aucun champ à mettre à jour.");
            }

            var setClause = string.Join(", ", setClauses);

            var query = $"UPDATE {GetNomTable()} SET {setClause} WHERE {whereClause}";

            using (var conn = ManaoConnexion())
            {
                try
                {
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        // Ajouter les paramètres pour les champs modifiés
                        foreach (var prop in properties)
                        {
                            var value = prop.GetValue(obj);
                            if (value != null)
                            {
                                cmd.Parameters.AddWithValue("@" + prop.Name, value);
                            }
                        }

                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Mise à jour réussie pour {GetNomTable()}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la mise à jour : {ex.Message}");
                    throw;
                }
            }
        }


    }
}
