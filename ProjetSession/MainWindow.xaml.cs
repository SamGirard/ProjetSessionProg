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
using System.Collections.ObjectModel;
using System.Globalization;
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
            bool connecter = Singleton.GetInstance().valideConnection();

            if (connecter == true)
            {
                iAjoutProjet.IsEnabled = true;
                iAjoutClient.IsEnabled = true;
                iAjoutEmpl.IsEnabled = true;
                iExport.IsEnabled = true;
                iDeco.Content = "Se déconnecter";
            }
            mainFrame.Navigate(typeof(PageAfficherProjet));

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

                case "iListeEmploye":
                    mainFrame.Navigate(typeof(PageAfficherEmploye));
                    break;

                default:
                    break;
            }

        }

        private async void iExport_Tapped(object sender, TappedRoutedEventArgs e)
        {
            

            
            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            picker.SuggestedFileName = "Liste des projets";
            picker.FileTypeChoices.Add("Fichier CSV", new List<string>() { ".csv" });

            //crée le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            if (monFichier == null)
            {
                return; 
            }

            string texteAEcrire = "idProjet;titre;date_debut;description;budget;nb_employe;salaireTotal;idClient;statut";

            ObservableCollection<Projet> listeDeDonnees = Singleton.GetInstance().GetListeProjet();

            // Parcourez la liste et ajoutez chaque ligne au fichier CSV
            foreach (var donnee in listeDeDonnees)
            {
                texteAEcrire += $"\n{donnee.IdProjet};{donnee.Titre};{donnee.DateDebut.ToString()};{donnee.Description};{donnee.Budget};{donnee.NbEmploye};{donnee.TotalSal};{donnee.IdCLient};{donnee.Statut}";
            }
       
                // Écrit dans le fichier
                await Windows.Storage.FileIO.WriteTextAsync(monFichier, texteAEcrire, Windows.Storage.Streams.UnicodeEncoding.Utf8);
        }

        private async void iDeco_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            if (Singleton.GetInstance().valideConnection() == false) { 
                connexion dialog = new connexion();
                dialog.XamlRoot = mainFrame.XamlRoot;
                dialog.Title = "Se connecter";
                dialog.PrimaryButtonText = "Connexion";
                dialog.CloseButtonText = "Annuler";
                dialog.DefaultButton = ContentDialogButton.Primary;
                ContentDialogResult result = await dialog.ShowAsync();


                if (result == ContentDialogResult.Primary)
                {
                    string utilisateur = dialog.User;
                    string motDePasse = dialog.Mdp;

                    bool estConnecter = Singleton.GetInstance().verif_Admin(utilisateur, motDePasse);

                    if (estConnecter == true)
                    {
                        iAjoutProjet.IsEnabled = true;
                        iAjoutClient.IsEnabled = true;
                        iAjoutEmpl.IsEnabled = true;
                        iDeco.Content = "Se déconnecter";
                        mainFrame.Navigate(typeof(PageAfficherProjet));
                        iListeProjet.IsSelected = true;
                    }
                }
            } 
            else
            {
                ContentDialog dialog2 = new ContentDialog();
                dialog2.XamlRoot = mainFrame.XamlRoot;
                dialog2.Title = "Déconnexion";
                dialog2.PrimaryButtonText = "Oui";
                dialog2.CloseButtonText = "Annuler";
                dialog2.DefaultButton = ContentDialogButton.Close;
                dialog2.Content = "Voulez-vous vraiment vous déconnecter?";

                var result2 = await dialog2.ShowAsync();

                if(result2 == ContentDialogResult.Primary)
                {
                    Singleton.GetInstance().deconnexion();
                    iAjoutProjet.IsEnabled = false;
                    iAjoutClient.IsEnabled = false;
                    iAjoutEmpl.IsEnabled = false;
                    iDeco.Content = "Se Connecter";
                    mainFrame.Navigate(typeof(PageAfficherProjet));
                    iListeProjet.IsSelected = true;
                }

            }
        }

        private async void iAjoutProjet_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AjoutProjetContent dialog = new AjoutProjetContent();
            dialog.XamlRoot = mainFrame.XamlRoot;
            dialog.Title = "Créer un nouveau projet";
            dialog.PrimaryButtonText = "Ajouter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            await dialog.ShowAsync();
        }


        private async void iAjoutEmpl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AjouterEmploye dialog = new AjouterEmploye();
            dialog.XamlRoot = mainFrame.XamlRoot;
            dialog.Title = "Ajouter un employé";
            dialog.PrimaryButtonText = "Ajouter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            ContentDialogResult result = await dialog.ShowAsync();
        }

        private async void iAjoutClient_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AjoutClientContent dialog = new AjoutClientContent();
            dialog.XamlRoot = mainFrame.XamlRoot;
            dialog.Title = "Ajouter un employé";
            dialog.PrimaryButtonText = "Ajouter";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            ContentDialogResult result = await dialog.ShowAsync();
        }
    }
}
