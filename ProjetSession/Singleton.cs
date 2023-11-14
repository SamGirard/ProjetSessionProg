using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //con = new MySqlConnection("Server=cours.cegep3r.info;Database=2172853-samuel-girard;Uid=2172853;Pwd=2172853");
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
    }
}
