using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession
{
    internal class Projet : INotifyPropertyChanged
    {
        string idProjet;
        string titre;
        string dateDebut;
        string description;
        double budget;
        int nbEmploye;
        double totalSal;
        string idCLient;
        string statut;

        public string IdProjet
        {
            get { return idProjet; }
            set { idProjet = value; this.OnPropertyChanged(); }
        }
        public string Titre
        {
            get { return titre; }
            set { titre = value; this.OnPropertyChanged(); }
        }
        public string DateDebut
        {
            get { return dateDebut; }
            set { dateDebut = value; this.OnPropertyChanged(); }
        }
        public string Description
        {
            get { return description; }
            set { description = value; this.OnPropertyChanged(); }
        }

        public double Budget
        {
            get { return budget; }
            set { budget = value; this.OnPropertyChanged(); }
        }

        public int NbEmploye
        {
            get { return nbEmploye; }
            set { nbEmploye = value; this.OnPropertyChanged(); }
        }

        public double TotalSal
        {
            get { return totalSal; }
            set { totalSal = value; this.OnPropertyChanged(); }
        }
        
        public string IdCLient
        {
            get { return idCLient; }
            set
            {
                idCLient = value; this.OnPropertyChanged();
            }
        }
        public string Statut
        {
            get { return statut; }
            set { statut = value; this.OnPropertyChanged(); }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
