using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

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
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2023_420325ri_fabeq19;Uid=2172853;Pwd=2172853");
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
                MySqlCommand commande = new MySqlCommand("afficher_projets");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();
                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    Projet unProjet = new Projet()
                    {
                        IdProjet = reader.GetString("id_projet"),
                        Titre = reader.GetString("titre"),
                        DateDebut = reader.GetString("date_debut"),
                        Description = reader.GetString("description"),
                        Budget = reader.GetInt32("budget"),
                        NbEmploye = reader.GetInt32("nb_employe"),
                        TotalSal = reader.GetInt32("salaireTotal"),
                        IdCLient = reader.GetString("id_client"),
                        Statut = reader.GetString("statut"),
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
                        Id_Client = reader.GetString("id_client"),
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


        public void AjouterProjet(string titre, DateTime dateDebut, string client, string description, int budget, int nbEmployé, string statut)
        {
            string idClient = "";
            try
            {
                MySqlCommand commande2 = new MySqlCommand();
                commande2.Connection = con;
                commande2.CommandText = "SELECT id_client FROM client WHERE nom LIKE @client";
                commande2.Parameters.AddWithValue("@client", client);

                con.Open();

                MySqlDataReader reader = commande2.ExecuteReader();

                while (reader.Read())
                {
                    idClient = reader.GetString("id_client");
                }
                reader.Close();


                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO projet values(null, @titre, @date_debut, @description, @budget, @nbEmploye, null, @statut, @client)";

                commande.Parameters.AddWithValue("@titre", titre);
                commande.Parameters.AddWithValue("@date_debut", dateDebut.ToString("yyyy-MM-dd"));
                commande.Parameters.AddWithValue("@id_client", client);
                commande.Parameters.AddWithValue("@description", description);
                commande.Parameters.AddWithValue("@budget", budget);
                commande.Parameters.AddWithValue("@nb_employe", nbEmployé);
                commande.Parameters.AddWithValue("@statut", statut);
                commande.Parameters.AddWithValue("@client", idClient);

                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception)
            {
                con.Close();
            }
        }

        public void AjouterEmploye(string nom, string prenom, DateTime date_naissance, string email, string adresse, DateTime date_embauche, double taux, string photo, string projet, string statut)
        {
            string idProjet = "";
            try
            {
                MySqlCommand commande2 = new MySqlCommand();
                commande2.Connection = con;
                commande2.CommandText = "SELECT id_projet FROM projet WHERE titre LIKE @projet";
                commande2.Parameters.AddWithValue("@id_projet", projet);

                con.Open();

                MySqlDataReader reader = commande2.ExecuteReader();

                while (reader.Read())
                {
                    idProjet = reader.GetString("id_projet");
                }
                reader.Close();


                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO employe values(null, @nom, @prenom, @date_naissance, @email, @adresse, @date_embauche, @taux, @photo, @id_projet, @statut)";

                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@prenom", prenom);
                commande.Parameters.AddWithValue("@date_naissance", date_naissance.ToString("yyyy-MM-dd"));
                commande.Parameters.AddWithValue("@email", email);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@date_embauche", date_embauche.ToString("yyyy-MM-dd"));
                commande.Parameters.AddWithValue("@taux", taux);
                commande.Parameters.AddWithValue("@photo", photo);
                commande.Parameters.AddWithValue("@id_projet", idProjet);
                commande.Parameters.AddWithValue("@statut", statut);

                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception)
            {
                con.Close();
            }
        }

        public List<string> GetNomsProjets()
        {
            List<string> titres = new List<string>();

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT titre FROM projet";

                con.Open();
                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    string titre = reader.GetString("titre");
                    titres.Add(titre);
                }

                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return titres;
        }


        public bool verif_Admin(string utilisateur, string motDePasse)
        {
            bool estConnecter = false;
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM admin";

                con.Open();
                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    if (utilisateur == reader.GetString("utilisateur") && motDePasse == reader.GetString("motDePasse"))
                    {
                        estConnecter = true;
                        break;
                    }

                }
                reader.Close();
                con.Close();
            }

            catch (Exception ex)
            {
                con.Close();
            }

            if (estConnecter == true)
            {
                try
                {
                    MySqlCommand commande2 = new MySqlCommand();
                    commande2.Connection = con;
                    commande2.CommandText = "UPDATE admin SET estConnecter = true WHERE utilisateur = @utilisateur";
                    commande2.Parameters.AddWithValue("@utilisateur", utilisateur);

                    con.Open();
                    int i = commande2.ExecuteNonQuery();

                    con.Close();
                }
                catch (Exception ex)
                {
                    con.Close();
                }
            }

            return estConnecter;
        }

        public bool valideConnection()
        {
            bool estConnecter = false;
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT estConnecter FROM admin";

                con.Open();
                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    if (reader.GetInt32("estConnecter") == 1)
                    {
                        estConnecter = true;
                        break;
                    }

                }
                reader.Close();
                con.Close();
            }

            catch (Exception ex)
            {
                con.Close();
            }
            return estConnecter;
        }

        public void deconnexion()
        {
            try
            {
                MySqlCommand commande2 = new MySqlCommand();
                commande2.Connection = con;
                commande2.CommandText = "UPDATE admin SET estConnecter = false WHERE 1 = 1";

                con.Open();
                int i = commande2.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
        }

        public void supprimer(Object objet, int position)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;

            if (objet.GetType() == typeof(Client))
            {
                Client client = (Client)objet;
                string id = client.Id_Client;
                try
                {
                    commande.CommandText = $"DELETE FROM client WHERE id_client='{id}'";
                    con.Open();
                    commande.ExecuteNonQuery();

                    con.Close();
                    listeClient.RemoveAt(position);
                }
                catch (Exception ex) { con.Close(); }
            }
            else if(objet.GetType() == typeof(Employe))
            {
                Employe employe = (Employe)objet;
                string matricule = employe.Matricule;
                try
                {
                    commande.CommandText = $"DELETE FROM employe WHERE matricule='{matricule}'";
                    con.Open();
                    commande.ExecuteNonQuery();

                    con.Close();
                    listeEmploye.RemoveAt(position);
                }
                catch (Exception ex) { con.Close(); }
            }
            else if (objet.GetType() == typeof(Projet))
            {
                Projet projet = (Projet)objet;
                string id = projet.IdProjet;
                try
                {
                    commande.CommandText = $"DELETE FROM projet WHERE id_projet='{id}'";
                    con.Open();
                    commande.ExecuteNonQuery();

                    con.Close();
                    listeProjet.RemoveAt(position);
                }
                catch (Exception ex) { con.Close(); }
            }
        }

    }
}
