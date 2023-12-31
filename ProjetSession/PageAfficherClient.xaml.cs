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
            /*POUR ACTIVER/D�SACTIVER LES BOUTONS*/
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

        private async void btModifier_Click(object sender, RoutedEventArgs e)
        {
            Client client = lvListe.SelectedItem as Client;
            AjoutClientContent dialog = new AjoutClientContent();
            dialog.XamlRoot = validation.XamlRoot;
            dialog.Title = "Modifier un client";
            dialog.PrimaryButtonText = "Modifier";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;

            dialog.IdClient = client.Id_Client;
            dialog.Nom = client.Nom;
            dialog.Adresse = client.Adresse;
            dialog.Num_Tel = client.Num_Tel;
            dialog.Email = client.Email;
            dialog.Modifier = 1;



            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                this.Frame.Navigate(typeof(PageAfficherClient));
            }
        }

        private async void btDelete_Click(object sender, RoutedEventArgs e)
        {
            int position = lvListe.SelectedIndex;
            Client client = lvListe.SelectedItem as Client;
            string id = client.Id_Client;
            string nom = client.Nom;

            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = validation.XamlRoot;
            dialog.Title = "Supprimer un client";
            dialog.PrimaryButtonText = "Supprimer";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;
            dialog.Content = $"�tes-vous s�re de vouloir supprimer le client : {nom}?";
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Singleton.GetInstance().supprimer(client, position);
                /*NE PAS OUBLIER DALLER CHANGER VALEUR IDCLIENT DANS PROJET POUR NULL*/
            }
        }
    }
}
