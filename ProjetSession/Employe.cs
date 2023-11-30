using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSession
{
    internal class Employe : INotifyPropertyChanged
    {
        string matricule;
        string nom;
        string prenom;
        string dateNaiss;
        string email;
        string adresse;
        string dateEmb;
        double tauxHor;
        string photo;
        string statut;
        string idProjet;
        Projet projet;

        public string Matricule
        {
            get { return matricule; }
            set { matricule = value; this.OnPropertyChanged(); }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; this.OnPropertyChanged(); }
        }

        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; this.OnPropertyChanged(); }
        }

        public string DateNaiss
        {
            get { return dateNaiss; }
            set { dateNaiss = value; this.OnPropertyChanged(); }
        }
        public string Email
        {
            get { return email; }
            set { email = value; this.OnPropertyChanged(); }
        }
        
        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; this.OnPropertyChanged(); }
        }
        public string DateEmb
        {
            get { return dateEmb; }
            set { dateEmb = value; this.OnPropertyChanged(); }
        }
        public double TauxHor
        {
            get { return tauxHor; }
            set { tauxHor = value; this.OnPropertyChanged(); }
        }
        public string Photo
        {
            get { return photo; }
            set { photo = value; this.OnPropertyChanged(); }
        }
        public string Statut
        {
            get { return statut; }
            set { statut = value; this.OnPropertyChanged(); }
        }

        public Projet ProjetEnCours
        {
            get { return projet; }
            set {  projet = value; this.OnPropertyChanged(); }
        }

        
        public string IdProjet
        {
            get { return idProjet; }
            set { idProjet = value; this.OnPropertyChanged(); }
        }

        public string NomComplet
        {
            get { return $"{prenom} {nom}"; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}