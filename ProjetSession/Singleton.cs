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

            /*string server = Environment.GetEnvironmentVariable("server");
            string database = Environment.GetEnvironmentVariable("database");
            string user = Environment.GetEnvironmentVariable("user");
            string password = Environment.GetEnvironmentVariable("password");
            */
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
                    Client client = getClient((string)reader["id_client"]);
                    Projet unProjet = new Projet()
                    {
                        IdProjet = reader.GetString("id_projet"),
                        Titre = reader.GetString("titre"),
                        DateDebutTest = reader.GetString("date_debut"),
                        Description = reader.GetString("description"),
                        Budget = reader.GetDouble("budget"),
                        NbEmploye = reader.GetInt32("nb_employe"),
                        TotalSal = reader.GetDouble("salaireTotal"),
                        IdCLient = reader.GetString("id_client"),
                        Statut = reader.GetString("statut"),
                        Client = client,
                        ListeEmploye = ListeEmployesProjet(reader.GetString("id_projet"))
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

        public List<Employe> ListeEmployesProjet(string idProjet)
        {
            List<Employe> liste = new List<Employe>();
            try
            {
                MySqlCommand commande = new MySqlCommand("p_liste_empl_projet");
                commande.Connection = con2;
                commande.CommandType = System.Data.CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("idProjet", idProjet);

                con2.Open();
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
                    liste.Add(unEmploye);
                }
                reader.Close();
                con2.Close();
            }
            catch (Exception ex) { con2.Close(); }
            return liste;
        }
        private Projet getProjet(string idProjet)
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

        private Client getClient(string idClient)
        {
            MySqlCommand commande = new MySqlCommand("p_get_client");
            commande.Connection = con2;
            commande.CommandType = System.Data.CommandType.StoredProcedure;

            commande.Parameters.AddWithValue("id", idClient);

            con2.Open();

            MySqlDataReader read2 = commande.ExecuteReader();
            read2.Read();
            string id = (string)read2["id_client"];
            string nom = (string)read2["nom"];
            string adresse = (string)read2["adresse"];
            string numero = (string)read2["numero_tel"];
            string email = (string)read2["email"];

            Client client = new Client
            {
                Id_Client = id,
                Nom = nom,
                Adresse = adresse,
                Num_Tel = numero,
                Email = email
            };

            read2.Close();
            con2.Close();
            return client;
        }


        /*****************************************************************************************************/
        /***************************************AJOUT/MODIFIER/SUPPRIMER**************************************/
        /*****************************************************************************************************/
        public void ajouter(Object objet)
        {
            ////////////////////AJOUT CLIENT\\\\\\\\\\\\\\\\\\\\
            if (objet is Client)
            {
                Client client = (Client)objet;
                try
                {
                    MySqlCommand commande = new MySqlCommand("p_ajout_client");
                    commande.Connection = con;
                    commande.CommandType = System.Data.CommandType.StoredProcedure;

                    commande.Parameters.AddWithValue("nom", client.Nom);
                    commande.Parameters.AddWithValue("adresse", client.Adresse);
                    commande.Parameters.AddWithValue("numero_tel", client.Num_Tel);
                    commande.Parameters.AddWithValue("email", client.Email);

                    con.Open();
                    commande.Prepare();
                    int i = commande.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex) { con.Close(); }
                listeClient.Add(client);
            }
            ////////////////////AJOUT EMPLOYÉ\\\\\\\\\\\\\\\\\\\\
            else if (objet is Employe)
            {
                Employe employe = (Employe)objet;
                try
                {
                    MySqlCommand commande = new MySqlCommand("p_ajout_employe");
                    commande.Connection = con;
                    commande.CommandType = System.Data.CommandType.StoredProcedure;

                    commande.Parameters.AddWithValue("nom", employe.Nom);
                    commande.Parameters.AddWithValue("prenom", employe.Prenom);
                    commande.Parameters.AddWithValue("date_naiss", employe.DateNaiss);
                    commande.Parameters.AddWithValue("email", employe.Email);
                    commande.Parameters.AddWithValue("adresse", employe.Adresse);
                    commande.Parameters.AddWithValue("date_emb", employe.DateEmb);
                    commande.Parameters.AddWithValue("taux", employe.TauxHor);
                    commande.Parameters.AddWithValue("photo", employe.Photo);
                    commande.Parameters.AddWithValue("idProjet", employe.IdProjet);
                    commande.Parameters.AddWithValue("statut", employe.Statut);

                    con.Open();
                    commande.Prepare();
                    int i = commande.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex) { con.Close(); }
                listeEmploye.Add(employe);
            }
            ////////////////////AJOUT PROJET\\\\\\\\\\\\\\\\\\\\
            else if (objet is Projet)
            {
                Projet projet = (Projet)objet;
                try
                {
                    MySqlCommand commande = new MySqlCommand("p_ajout_projet");
                    commande.Connection = con;
                    commande.CommandType = System.Data.CommandType.StoredProcedure;

                    commande.Parameters.AddWithValue("titre", projet.Titre);
                    commande.Parameters.AddWithValue("date_debut", projet.DateDebut);
                    commande.Parameters.AddWithValue("description", projet.Description);
                    commande.Parameters.AddWithValue("budget", projet.Budget);
                    commande.Parameters.AddWithValue("nbEmplo", projet.NbEmploye);
                    commande.Parameters.AddWithValue("id_client", projet.IdCLient);
                    commande.Parameters.AddWithValue("statut", projet.Statut);

                    con.Open();
                    commande.Prepare();
                    int i = commande.ExecuteNonQuery();
                    con.Close();
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



        public void modifier(Object objet, int position)
        {
            if (objet is Client)
            {
                Client client = objet as Client;
            }
            else if(objet is Employe)
            {
                Employe employe = objet as Employe;
            }
            else if(objet is Projet)
            {
                Projet projet = (Projet)objet;
            }
        }

        /*****************************************************************************************************/
        /**********************************************CONNEXION**********************************************/
        /*****************************************************************************************************/

        public bool compteExiste()
        {
            bool existe = false;
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "SELECT COUNT(*) FROM afficher_admin";

                con.Open();
                int nombreDeLignes = Convert.ToInt32(commande.ExecuteScalar());

                if (nombreDeLignes <= 0)
                {
                    existe = false;
                }
                else existe = true;

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return existe;
        }

        public void creerCompte(string utilisateur, string mdp)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "INSERT INTO admin VALUES(@utilisateur, @mdp, 0)";

            commande.Parameters.AddWithValue("utilisateur", utilisateur);
            commande.Parameters.AddWithValue("motDePasse", mdp);

            con.Open();
            commande.ExecuteNonQuery();

            con.Close();
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
                    MySqlCommand commande2 = new MySqlCommand("p_estConnecter");
                    commande2.Connection = con;
                    commande2.CommandType = System.Data.CommandType.StoredProcedure;
                    commande2.Parameters.AddWithValue("@compte", utilisateur);

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
                MySqlCommand commande = new MySqlCommand("p_deconnexion");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();
                int i = commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
        }



        /*****************************************************************************************************/
        /***********************************************AUTRES************************************************/
        /*****************************************************************************************************/
        public int GetPositionProjet(string idProjet)
        {
            ObservableCollection<Projet> listeProjet2 = GetInstance().GetListeProjet();
            for (int i = 0; i < listeProjet2.Count; i++)
            {
                Projet projet = listeProjet2[i];
                string id = projet.IdProjet;
                if (id == idProjet)
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetPositionClient(string idClient)
        {
            ObservableCollection<Client> listeClient2 = GetInstance().GetListeClient();
            for (int i = 0; i < listeClient2.Count; i++)
            {
                Client client = listeClient2[i];
                string id = client.Id_Client;
                if (id == idClient)
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
