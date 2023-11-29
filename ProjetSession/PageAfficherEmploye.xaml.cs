using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
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
            /*POUR ACTIVER/DÉSACTIVER LES BOUTONS*/
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
            dialog.Title = "Modifier un employé existant";
            dialog.PrimaryButtonText = "Modifier";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Nom = employe.Nom;
            dialog.Prenom = employe.Prenom;
            dialog.Date_Naissance = employe.DateNaiss;
            dialog.Email = employe.Email;
            dialog.Adresse = employe.Adresse;
            dialog.Date_Embauche = employe.DateEmb;
            dialog.Taux = Convert.ToString(employe.TauxHor);
            dialog.Photo = employe.Photo;

            dialog.IdProjet = Singleton.GetInstance().GetPositionEmpl(employe.IdProjet);

            await dialog.ShowAsync();
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
