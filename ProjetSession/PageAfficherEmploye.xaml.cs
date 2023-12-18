using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace ProjetSession
{
    public sealed partial class PageAfficherEmploye : Page
    {
        public PageAfficherEmploye()
        {
            this.InitializeComponent();
            gvListe.ItemsSource = Singleton.GetInstance().GetListeEmploye();

            /*Affichage bouton admin*/
            if (Singleton.GetInstance().valideConnection())
            {
                btModifier.Visibility = Visibility.Visible;
                btDelete.Visibility = Visibility.Visible;
            }
            else
            {
                btModifier.Visibility = Visibility.Collapsed;
                btDelete.Visibility = Visibility.Collapsed;
            }

        }

        private void gvListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*ACTIVER/DÉSACTIVER LES BOUTONS*/
            if (Singleton.GetInstance().valideConnection())
            {
                if (gvListe.SelectedIndex > -1)
                {
                    btModifier.IsEnabled = true;
                    btDelete.IsEnabled = true;
                }
                else if (gvListe.SelectedIndex == -1)
                {
                    btModifier.IsEnabled = false;
                    btDelete.IsEnabled = false;
                }
            }
        }

        private async void btModifier_Click(object sender, RoutedEventArgs e)
        {
            Employe employe = gvListe.SelectedItem as Employe;
            AjouterEmploye dialog = new AjouterEmploye();
            dialog.XamlRoot = validation.XamlRoot;
            dialog.Title = "Modifier un employé";
            dialog.PrimaryButtonText = "Modifier";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            dialog.CdpNaiss.IsEnabled = false;
            dialog.CdpEmb.IsEnabled = false;

            dialog.Nom = employe.Nom;
            dialog.Prenom = employe.Prenom;
            dialog.Date_Naissance = employe.DateNaissTest;
            dialog.Email = employe.Email;
            dialog.Adresse = employe.Adresse;
            dialog.Date_Embauche = employe.DateEmb;
            dialog.Taux = Convert.ToString(employe.TauxHor);
            dialog.Photo = employe.Photo;
            dialog.IdProjet = Singleton.GetInstance().GetPositionProjet(employe.IdProjet);
            dialog.Modifier = true;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Singleton.GetInstance().modifier(employe);
                this.Frame.Navigate(typeof(PageAfficherEmploye));
            }
        }

        private async void btDelete_Click(object sender, RoutedEventArgs e)
        {
            int position = gvListe.SelectedIndex;
            Employe employe = gvListe.SelectedItem as Employe;
            string id = employe.Matricule;
            string nom = employe.NomComplet;

            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = validation.XamlRoot;
            dialog.Title = "Supprimer un(e) employé";
            dialog.PrimaryButtonText = "Supprimer";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;
            dialog.Content = $"Êtes-vous sûre de vouloir supprimer l'employé : {nom}?";
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Singleton.GetInstance().supprimer(employe, position);
            }
        }
    }
}
