using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace ProjetSession
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void navView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var items = (NavigationViewItem)args.SelectedItem;

            switch (items.Name)
            {
                case "iListeProjet":
                    mainFrame.Navigate(typeof(PageAfficherProjet));
                    break;

                case "iListeClient":
                    mainFrame.Navigate(typeof(PageAfficherClient));
                    break;

                default:
                    break;
            }

        }


        private void iImport_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void iExport_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private async void iDeco_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var connexions = false;

            connexion dialog = new connexion();
            dialog.XamlRoot = dialogAjout.XamlRoot;
            dialog.Title = "Se connecter";
            dialog.PrimaryButtonText = "Connexion";
            dialog.CloseButtonText = "Annuler";
            await dialog.ShowAsync();
            ContentDialogResult result = await dialog.ShowAsync();


            if (result == ContentDialogResult.Primary)
            {
                var erreur = false;

                string utilisateur = dialog.User;
                string motDePasse = dialog.Mdp;



                if (erreur == false)
                {
                    Singleton.GetInstance().verif_Admin(utilisateur, motDePasse);
                }
            }

            if (connexions == true)
            {
                iAjoutProjet.IsEnabled = true;
                iAjoutClient.IsEnabled = true;
                iAjoutEmpl.IsEnabled = true;
            }
        }

        private async void iAjoutProjet_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AjoutProjetContent dialog = new AjoutProjetContent();
            dialog.XamlRoot = dialogAjout.XamlRoot;
            dialog.Title = "Créer un nouveau projet";
            dialog.PrimaryButtonText = "Ajouter";
            dialog.CloseButtonText = "Annuler";
            await dialog.ShowAsync();
        }


        private async void iAjoutEmpl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AjouterEmploye dialog = new AjouterEmploye();
            dialog.XamlRoot = dialogAjout.XamlRoot;
            dialog.Title = "Ajouter un employé";
            dialog.PrimaryButtonText = "Ajouter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            ContentDialogResult result = await dialog.ShowAsync();
        }

        private async void iAjoutClient_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AjoutClientContent dialog = new AjoutClientContent();
            dialog.XamlRoot = dialogAjout.XamlRoot;
            dialog.Title = "Ajouter un employé";
            dialog.PrimaryButtonText = "Ajouter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            ContentDialogResult result = await dialog.ShowAsync();
        }
    }
}
