using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace ProjetSession
{
    public sealed partial class PageAfficherProjet : Page
    {
        public PageAfficherProjet()
        {
            this.InitializeComponent();
            gvListe.ItemsSource = Singleton.GetInstance().GetListeProjet();

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
            Projet projet = gvListe.SelectedItem as Projet;
            AjoutProjetContent dialog = new AjoutProjetContent();
            dialog.XamlRoot = validation.XamlRoot;
            dialog.Title = "Modifier un projet";
            dialog.PrimaryButtonText = "Modifier";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            
            dialog.Titre = projet.Titre;
            dialog.Description = projet.Description;
            dialog.Budget = Convert.ToString(projet.Budget);
            dialog.NbEmploye = Convert.ToString(projet.NbEmploye);
            dialog.IdClient = Singleton.GetInstance().GetPositionClient(projet.IdCLient);
            if (projet.Statut == "En cours")
            {
                dialog.Statut = 0;
            }
            else if (projet.Statut == "Terminé")
            {
                dialog.Statut = 1;
            }
            else
            {
                dialog.Statut = -1;
            }

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                /*Va rester a faire le singleton et l'appeler ici*/
            }
        }

        
        private async void btDelete_Click(object sender, RoutedEventArgs e)
        {
            
            int position = gvListe.SelectedIndex;
            Projet projet = gvListe.SelectedItem as Projet;
            string id = projet.IdProjet;
            string nom = projet.Titre;

            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = validation.XamlRoot;
            dialog.Title = "Supprimer un projet";
            dialog.PrimaryButtonText = "Supprimer";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;
            dialog.Content = $"Êtes-vous sûre de vouloir supprimer le projet : {projet}?";
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Singleton.GetInstance().supprimer(projet, position);
                /*********VA DEVOIR ALLER CHANGER LES EMPLOYÉS LIÉ AU PROJET (changer leur valeur pour null)************/

            }

        }

        private void statut_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if (textBlock != null)
            {
                // Accédez à l'Ellipse par son nom
                Ellipse ellipse = FindName("couleur") as Ellipse;

                if (ellipse != null)
                {
                    // Changez la couleur de l'Ellipse en fonction du Statut
                    string statut = textBlock.Text;

                    switch (statut)
                    {
                        case "En cours":
                            ellipse.Fill = new SolidColorBrush(Colors.Red);
                            break;
                        case "Terminé":
                            ellipse.Fill = new SolidColorBrush(Colors.Green);
                            break;
                        // Ajoutez d'autres cas selon vos besoins
                        default:
                            ellipse.Fill = new SolidColorBrush(Colors.Gray);
                            break;
                    }
                }
            }
        }

    }
}
