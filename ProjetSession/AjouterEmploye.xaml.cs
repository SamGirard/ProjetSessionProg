using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace ProjetSession
{
    public sealed partial class AjouterEmploye : ContentDialog
    {

        string nom = "";
        string prenom = "";
        DateTime dateNaissance;
        string email = "";
        string adresse = "";
        DateTime dateEmbauche;
        string photo = "";
        string statut = "";


        public AjouterEmploye()
        {
            this.InitializeComponent();
            cbxProjet.ItemsSource = Singleton.GetInstance().GetNomsProjets();
        }


        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var erreur = false;

            if (tbxNom.Text == "")
            {
                errNom.Text = "Le nom est vide";
                erreur = true;
                args.Cancel = true;
            }
            else {
                errNom.Text = "";
                nom = tbxNom.Text;
            }

            if (tbxPrenom.Text == "")
            {
                errPrenom.Text = "Le prenom est vide";
                erreur = true;
                args.Cancel = true;
            }
            else
            {
                errPrenom.Text = "";
                prenom = tbxPrenom.Text;
            }


            if (tbxEmail.Text == "")
            {
                errEmail.Text = "L'email est vide";
                erreur = true;
                args.Cancel = true;
            }
            else
            {
                errEmail.Text = "";
                email = tbxEmail.Text;
            }


            if (tbxAdresse.Text == "")
            {
                errAdresse.Text = "L'adresse est vide";
                erreur = true;
                args.Cancel = true;
            }
            else
            {
                errAdresse.Text = "";
                adresse = tbxAdresse.Text;
            }


            if (tbxTaux.Text == "")
            {
                errTaux.Text = "Le taux est vide";
                erreur = true;
                args.Cancel = true;
            }
            else errTaux.Text = "";

            if (cdpNaiss.Date == DateTimeOffset.MinValue)
            {
                erreur = true;
                errDate.Text = "La date est vide";
                args.Cancel = true;
            }
            else
            {
                dateNaissance = cdpNaiss.Date.Value.Date;
                errDate.Text = "";
            }

            if (cdpEmb.Date == DateTimeOffset.MinValue)
            {
                erreur = true;
                errEmbauche.Text = "La date est vide";
                args.Cancel = true;
            }
            else
            {
                dateEmbauche = cdpEmb.Date.Value.Date;
                errEmbauche.Text = "";
            }

            if (tbxPhoto.Text == "")
            {
                errPhoto.Text = "La photo est vide";
                erreur = true;
                args.Cancel = true;
            }
            else
            {
                errPhoto.Text = "";
                photo = tbxPhoto.Text;
            }


            if (cbxStatut.SelectedIndex == -1)
            {
                errStatut.Text = "Le statut est vide";
                erreur = true;
                args.Cancel = true;
            }
            else
            {
                errStatut.Text = "";
                statut = cbxStatut.SelectedItem.ToString();
            }


            if (erreur == false)
            {
                double dTaux = Convert.ToDouble(tbxTaux.Text);
                string projet = cbxProjet.SelectedItem.ToString();

                Singleton.GetInstance().AjouterEmploye(nom, prenom, dateNaissance, email, adresse, dateEmbauche, dTaux, photo, projet, statut);
            }
        }

        public string Nom
        {
            get { return tbxNom.Text; }
            set { tbxNom.Text = value; }
        }

        public string Prenom
        {
            get { return tbxPrenom.Text; }
            set { tbxPrenom.Text = value; }
        }

        /*
         DateTime selectedDate = calendarDatePicker1.Date.DateTime;
         string formattedDate = selectedDate.ToString("yyyy-MM-dd");
         */
        public string Date_Naissance
        {
            get { return cdpNaiss.Date.ToString(); }
            set 
            {
                DateTimeOffset date = new DateTimeOffset(DateTime.Parse(value));
                cdpNaiss.Date = date; 
            }
        }

        public string Email 
        { 
            get { return tbxEmail.Text; } 
            set { tbxEmail.Text = value;}
        }

        public string Adresse
        {
            get { return tbxAdresse.Text;}
            set { tbxAdresse.Text = value;}
        }

        public string Date_Embauche
        {
            get { return cdpEmb.Date.ToString(); }
            set
            {
                DateTimeOffset date = new DateTimeOffset(DateTime.Parse(value));
                cdpEmb.Date = date;
            }
        }

        public string Taux
        {
            get { return tbxTaux.Text; }
            set { tbxTaux.Text = value;}
        }
        public string Photo
        {
            get { return tbxPhoto.Text; }
            set { tbxPhoto.Text = value; }
        }

        public int IdProjet
        {
            get { return cbxProjet.SelectedIndex; }
            set {  cbxProjet.SelectedIndex = value;}
        }

        public int Statut
        {
            get { return cbxStatut.SelectedIndex; }
            set { cbxStatut.SelectedIndex = value;}
        }

    }
}
