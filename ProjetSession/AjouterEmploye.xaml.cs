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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetSession
{
    public sealed partial class AjouterEmploye : ContentDialog
    {

        string nom = "";
        string prenom = "";
        DateFormat dateNaissance;
        string email = "";
        string adresse = "";
        DateFormat dateEmbauche;
        string photo = "";
        string statut = "";


        public AjouterEmploye()
        {
            this.InitializeComponent();
        }


        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var erreur = false;

            if (tbxNom.Text == "")
            {
                errNom.Text = "Le nom est vide";
                erreur = true;
            }
            else {
                errNom.Text = "";
                nom = tbxNom.Text;
            }

            if (tbxPrenom.Text == "")
            {
                errPrenom.Text = "Le prenom est vide";
                erreur = true;
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
            }
            else errTaux.Text = "";



            if (tbxPhoto.Text == "")
            {
                errPhoto.Text = "La photo est vide";
                erreur = true;
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
            }
            else
            {
                errStatut.Text = "";
                statut = cbxStatut.SelectedItem.ToString();
            }


            if (erreur == false)
            {
                int intTaux = Convert.ToInt32(tbxTaux.Text);

                Singleton.GetInstance().AjouterEmploye(nom, prenom, dateNaissance, email, adresse, dateEmbauche, intTaux, photo, statut);
            }
        }
    }
}
