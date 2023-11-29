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
using static System.Net.Mime.MediaTypeNames;


namespace ProjetSession
{
    public sealed partial class PageAfficherClient : Page
    {
        public PageAfficherClient()
        {
            this.InitializeComponent();

            lvListe.ItemsSource = Singleton.GetInstance().GetListeClient();
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

        private void lvListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*POUR ACTIVER/DÉSACTIVER LES BOUTONS*/
            if (Singleton.GetInstance().valideConnection())
            {
                if (lvListe.SelectedIndex > -1)
                {
                    btModifier.IsEnabled = true;
                    btDelete.IsEnabled = true;
                }
                else if (lvListe.SelectedIndex == -1)
                {
                    btModifier.IsEnabled = false;
                    btDelete.IsEnabled = false;
                }
            }
        }

        private void btModifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btDelete_Click(object sender, RoutedEventArgs e)
        {
            int position = lvListe.SelectedIndex;
            Client client = lvListe.SelectedItem as Client;
            string id = client.Id_Client;
            string nom = client.Nom;

            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = validation.XamlRoot;
            dialog.Title = "Supprimer un client(e)?";
            dialog.PrimaryButtonText = "Supprimer";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = $"Êtes-vous sûre de vouloir supprimer le client : {nom}?";
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Singleton.GetInstance().supprimer(client, position);
            }
        }
    }
}
