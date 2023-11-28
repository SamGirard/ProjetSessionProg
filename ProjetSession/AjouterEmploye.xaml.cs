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

            if (dtDateNaiss.Date == DateTimeOffset.MinValue)
            {
                erreur = true;
                errDate.Text = "La date est vide";
                args.Cancel = true;
            }
            else
            {
                dateNaissance = dtDateNaiss.Date.Value.Date;
                errDate.Text = "";
            }

            if (calDateEmb.Date == DateTimeOffset.MinValue)
            {
                erreur = true;
                errEmbauche.Text = "La date est vide";
                args.Cancel = true;
            }
            else
            {
                dateEmbauche = calDateEmb.Date.Value.Date;
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
    }
}
