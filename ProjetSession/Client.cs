using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession
{
    internal class Client : INotifyPropertyChanged
    {
        string id_client;
        string nom;
        string adresse;
        string num_tel;
        string email;

        public string Id_Client
        {
            get { return id_client; }
            set { id_client = value; this.OnPropertyChanged(); }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; this.OnPropertyChanged(); }
        }

        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; this.OnPropertyChanged(); }
        }

        public string Num_Tel
        {
            get { return num_tel; }
            set { num_tel = value; this.OnPropertyChanged(); }
        }

        public string Email
        {
            get { return email; }
            set { email = value; this.OnPropertyChanged();}
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
