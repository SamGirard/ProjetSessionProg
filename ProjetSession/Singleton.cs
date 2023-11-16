using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;

namespace ProjetSession
{
    internal class Singleton
    {
        private ObservableCollection<Projet> listeProjet;
        private ObservableCollection<Client> listeClient;
        private ObservableCollection<Employe> listeEmploye;
        MySqlConnection con;

        private static Singleton instance;

        private Singleton()
        {
            listeProjet = new ObservableCollection<Projet>();
            listeClient = new ObservableCollection<Client>();
            listeEmploye = new ObservableCollection<Employe>();
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=2172853-girard-samuel;Uid=2172853;Pwd=2172853");
        }

        public static Singleton GetInstance()
        {
            if (instance == null)
                instance = new Singleton();

            return instance;
        }

        public ObservableCollection<Projet> GetListeProjet()
        {
            listeProjet.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM projet";

                con.Open();
                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    Projet unProjet = new Projet()
                    {
                        IdProjet = reader.GetString("numero"),
                        Titre = reader.GetString("titre"),
                        DateDebut = reader.GetString("date_debut"),
                        Description = reader.GetString("description"),
                        Budget = reader.GetInt32("budget"),
                        NbEmploye = reader.GetInt32("nb_employeRequis"),
                        TotalSal = reader.GetInt32("salaireTotal"),
                        IdCLient = reader.GetString("client"),
                        Statut = reader.GetString("status"),

                    };

                    listeProjet.Add(unProjet);
                }
                reader.Close();
                con.Close();  
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return listeProjet;
        }


        public ObservableCollection<Client> GetListeClient()
        {
            listeClient.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM client";

                con.Open();
                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    Client unClient = new Client()
                    {
                        Id_Client = reader.GetString("identifiant"),
                        Nom = reader.GetString("nom"),
                        Adresse = reader.GetString("adresse"),
                        Num_Tel = reader.GetString("numero_tel"),
                        Email = reader.GetString("email"),
                    };

                    listeClient.Add(unClient);
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return listeClient;
        }

        public ObservableCollection<Employe> GetListeEmploye()
        {
            listeEmploye.Clear();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM employe";

                con.Open();
                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    Employe unEmploye = new Employe()
                    {
                        Matricule = reader.GetString("matricule"),
                        Nom = reader.GetString("nom"),
                        Prenom = reader.GetString("prenom"),
                        DateNaiss = reader.GetString("date_naissance"),
                        Email = reader.GetString("email"),
                        Adresse = reader.GetString("adresse"),
                        DateEmb = reader.GetString("date_embauche"),
                        TauxHor = reader.GetInt32("taux"),
                        Photo = reader.GetString("photo"),
                        Statut = reader.GetString("statut"),
                    };

                    listeEmploye.Add(unEmploye);
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return listeEmploye;
        }


        public void AjouterEmploye(string nom, string prenom, DateTime date_naissance, string email, string adresse, DateTime dateEmbauche, int taux, string photo, string statut)
        {
            try
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO employe values(null, @nom, @prenom, @date_naissance, @email, @adresse, @date_embauche, @taux, @photo, @statut)";

                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prenom", prenom);
                commande.Parameters.AddWithValue("@date_naissance", date_naissance);
                commande.Parameters.AddWithValue("@email", email);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@date_embauche", dateEmbauche);
                commande.Parameters.AddWithValue("@taux", taux);
                commande.Parameters.AddWithValue("@photo", photo);
                commande.Parameters.AddWithValue("@statut", statut);


                con.Open();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception)
            {
                con.Close();
            }
        }


        public void AjouterProjet(string titre, DateTime dateDebut, string client, string description, int budget, int nbEmployé, string statut)
        {
            try
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO projet values(null, @titre, @date_debut, @description, @budget, @nbEmploye, null, @client, @statut)";

                commande.Parameters.AddWithValue("@titre", titre);
                commande.Parameters.AddWithValue("@date_debut", dateDebut);
                commande.Parameters.AddWithValue("@date_naissance", client);
                commande.Parameters.AddWithValue("@email", description);
                commande.Parameters.AddWithValue("@adresse", budget);
                commande.Parameters.AddWithValue("@date_embauche", nbEmployé);
                commande.Parameters.AddWithValue("@statut", statut);


                con.Open();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception)
            {
                con.Close();
            }
        }

    }
}
