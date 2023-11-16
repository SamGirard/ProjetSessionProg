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

        private static Singleton instance;

        MySqlConnection con;

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

                return listeProjet;
            }
            catch (Exception ex)
            {
                con.Close();
                return null;
            }
        }


        public ObservableCollection<Client> GetListeClient()
        {
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

                return listeClient;
            }
            catch (Exception ex)
            {
                con.Close();
                return null;
            }
        }

        public ObservableCollection<Employe> GetListeEmploye()
        {
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
                        Matricule = Convert.ToInt32(reader.GetString("matricule")),
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

                return listeEmploye;
            }
            catch (Exception ex)
            {
                con.Close();
                return null;
            }
        }


        public void AjouterEmploye(string nom, string prenom, DateFormat date_naissance, string email, string adresse, DateFormat dateEmbauche, int taux, string photo, string statut)
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

    }
}
