using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
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
        MySqlConnection con2;

        private static Singleton instance;

        private Singleton()
        {
            listeProjet = new ObservableCollection<Projet>();
            listeClient = new ObservableCollection<Client>();
            listeEmploye = new ObservableCollection<Employe>();
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2023_420325ri_fabeq19;Uid=2172853;Pwd=2172853");
            con2 = new MySqlConnection("Server=cours.cegep3r.info;Database=a2023_420325ri_fabeq19;Uid=2172853;Pwd=2172853");
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
                        Budget = reader.GetDouble("budget"),
                        NbEmploye = reader.GetInt32("nb_employe"),
                        TotalSal = reader.GetDouble("salaireTotal"),
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
                MySqlCommand commande = new MySqlCommand("afficher_clients");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

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
                MySqlCommand commande = new MySqlCommand("afficher_employes");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();
                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    Projet projet = getProjet((string)reader["id_projet"]);
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
                        IdProjet = reader.GetString("id_projet"),
                        
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


        public void AjouterProjet(string titre, DateTime dateDebut, string client, string description, int budget, int nbEmploye, string statut)
        {
            string idClient = "";
            try
            {
                MySqlCommand commande2 = new MySqlCommand("idClientPourAjouterProjet");
                commande2.Connection = con;
                commande2.CommandType = System.Data.CommandType.StoredProcedure;
                commande2.Parameters.AddWithValue("@idClient", client);

                con.Open();

                MySqlDataReader reader = commande2.ExecuteReader();

                while (reader.Read())
                {
                    idClient = reader.GetString("id_client");
                }
                reader.Close();

                MySqlCommand commande = new MySqlCommand("p_ajout_projet");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("@titre", titre);
                commande.Parameters.AddWithValue("@date_debut", dateDebut.ToString("yyyy-MM-dd"));
                commande.Parameters.AddWithValue("@description", description);
                commande.Parameters.AddWithValue("@budget", budget);
                commande.Parameters.AddWithValue("@nbEmplo", nbEmploye);
                commande.Parameters.AddWithValue("@id_client", idClient);
                commande.Parameters.AddWithValue("@statut", statut);

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

            MySqlCommand commande2 = new MySqlCommand();
            commande2.Connection = con;
            commande2.CommandText = "SELECT id_projet FROM projet WHERE titre LIKE @projet";
            commande2.Parameters.AddWithValue("@projet", projet);

            con.Open();

            MySqlDataReader reader = commande2.ExecuteReader();

            while (reader.Read())
            {
                idProjet = reader.GetString("id_projet");
            }
            reader.Close();
            con.Close();

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO employe VALUES(null, @nom, @prenom, @date_naissance, @email, @adresse, @date_embauche, @taux, @photo, @id_projet, @statut)";

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

                con.Open();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception e)
            {
                con.Close();
            }
        }

        public void AjouterClient(string nom, string adresse, string numero, string email)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "INSERT INTO client VALUES (null, @nom, @adresse, @numero_tel, @email)";

                commande.Parameters.AddWithValue("@nom", nom);
                commande.Parameters.AddWithValue("@adresse", adresse);
                commande.Parameters.AddWithValue("@numero_tel", numero);
                commande.Parameters.AddWithValue("@email", email);

                con.Open();
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

        public List<string> GetNomsEmployes()
        {
            List<string> noms = new List<string>();

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT * FROM employe";

                con.Open();
                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    string nom = reader.GetString("nom");
                    string prenom = reader.GetString("prenom");
                    noms.Add($"{prenom} {nom}");
                }

                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return noms;
        }

        public List<string> GetNomsClients()
        {
            List<string> noms = new List<string>();

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT nom FROM client";

                con.Open();
                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    string nom = reader.GetString("nom");
                    noms.Add(nom);
                }

                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }

            return noms;
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

        public Projet getProjet(string idProjet)
        {
            MySqlCommand commande = new MySqlCommand("p_get_projet");
            commande.Connection = con2;
            commande.CommandType = System.Data.CommandType.StoredProcedure;

            commande.Parameters.AddWithValue("id", idProjet);

            con2.Open();

            MySqlDataReader read2 = commande.ExecuteReader();
            read2.Read();
            string id = (string)read2["id_projet"];
            string titre = (string)read2["titre"];
            DateTime test = (DateTime)read2["date_debut"];
            string date = test.Date.ToString();
            string description = (string)read2["description"];
            double budget = (double)read2["budget"];
            int nbEmpl = (int)read2["nb_employe"];
            double salTot = (double)read2["salaireTotal"];
            string idClient = (string)read2["id_client"];
            string statut = (string)read2["statut"];

            Projet projet = new Projet
            {
                IdProjet = id,
                Titre = titre,
                DateDebut = date,
                Description = description,
                Budget = budget,
                NbEmploye = nbEmpl,
                TotalSal = salTot,
                IdCLient = idClient,
                Statut = statut
            };

            read2.Close();
            con2.Close();
            return projet;
        }

        public void ajouter(Object objet)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            if (objet is Client)
            {
                Client client = (Client)objet;
                string nom = client.Nom;
                string adresse = client.Adresse;
                string numero = client.Num_Tel;
                string email = client.Email;

                try
                {
                    commande.CommandText = "INSERT INTO client VALUES (null, @nom, @adresse, @numero_tel, @email)";

                    commande.Parameters.AddWithValue("@nom", nom);
                    commande.Parameters.AddWithValue("@adresse", adresse);
                    commande.Parameters.AddWithValue("@numero_tel", numero);
                    commande.Parameters.AddWithValue("@email", email);

                    con.Open();
                    commande.ExecuteNonQuery();

                    con.Close();
                }
                catch (Exception ex) { con.Close(); }
                listeClient.Add(client);

            }
            else if (objet is Employe)
            {
                Employe employe = (Employe)objet;
                string nom = employe.Nom;
                string prenom = employe.Prenom;
                /*AVAIT ARRETER ICI, tu voulais centraliser les ajouts en une methode, va voir exercice procedure pour avoir exemple*/

                try
                {

                }
                catch (Exception ex) { con.Close(); }
            }
            else if (objet is Projet)
            {
                Projet projet = (Projet)objet;
                string id = projet.IdProjet;

                try
                {

                }
                catch (Exception ex) { con.Close(); }
            }
        }

        public void supprimer(Object objet, int position)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;

            if (objet is Client)
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
            else if(objet is Employe)
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
            else if (objet is Projet)
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

        public int GetPositionEmpl(string idProjet)
        {
            ObservableCollection<Projet> listeProjet2 = GetInstance().GetListeProjet();
            for (int i = 0; i < listeProjet2.Count; i++)
            {
                Projet projet = listeProjet2[i];
                string id = projet.IdProjet;
                if(id == idProjet)
                {
                    return i;
                }
            }
            return -1;
        }

        public string GetProjetEnCours(string idProjet)
        {
            string nomProjet = "";
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT titre FROM projet WHERE id_projet LIKE @idProjet";
                commande.Parameters.AddWithValue("@idProjet", idProjet);


                con.Open();
                MySqlDataReader reader = commande.ExecuteReader();

                while (reader.Read())
                {
                    nomProjet = reader.GetString("titre");
                }
                reader.Close();
                con.Close();
            }
            

            catch (Exception ex)
            {
                con.Close();
            }
            return nomProjet;
        }

        public string GetNomEmploye(string idProjet)
        {
            List<String> listeNom = new List<String>();
            string nom = "";

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "SELECT CONCAT(prenom, ' ', nom) as nomEmp FROM employe WHERE id_projet LIKE @idProjet";
            commande.Parameters.AddWithValue("@idProjet", idProjet);


            con.Open();
            MySqlDataReader reader = commande.ExecuteReader();

            while (reader.Read())
            {
                nom = reader.GetString("nomEmp");

                listeNom.Add(nom);
            }
            reader.Close();
            con.Close();

            return listeNom.ToString();
        }

    }
}
