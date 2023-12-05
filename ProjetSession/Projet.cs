using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
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
        DateTime dateDebutTest;
        string description;
        double budget;
        int nbEmploye;
        double totalSal;
        string idCLient;
        string statut;
        Client client;

        private string _statut;

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

        public string DateDebutTest
        {
            get {  return dateDebutTest.Date.ToString("yyyy-MM-dd"); }
            set { dateDebutTest = DateTime.Parse(value); this.OnPropertyChanged(); }
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
            get { return _statut; }
            set
            {
                if (_statut != value)
                {
                    _statut = value;
                    OnPropertyChanged(nameof(Statut));
                    UpdateEllipseColor();
                }
            }
        }

        public Client Client
        {
            get { return client; }
            set { client = value; this.OnPropertyChanged(); }
        }

        public string TotalSalString
        {
            get { return totalSal.ToString() + "$/heure"; }
        }

        public string BudgetString
        {
            get { return totalSal.ToString("C"); }
        }

        private SolidColorBrush _ellipseColor;
        public SolidColorBrush EllipseColor
        {
            get { return _ellipseColor; }
            set
            {
                if (_ellipseColor != value)
                {
                    _ellipseColor = value;
                    OnPropertyChanged(nameof(EllipseColor));
                }
            }
        }

        private void UpdateEllipseColor()
        {
            switch (Statut)
            {
                case "En cours":
                    EllipseColor = new SolidColorBrush(Colors.DodgerBlue);
                    break;
                case "Terminé":
                    EllipseColor = new SolidColorBrush(Colors.LimeGreen);
                    break;
                default:
                    EllipseColor = new SolidColorBrush(Colors.Gray);
                    break;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
