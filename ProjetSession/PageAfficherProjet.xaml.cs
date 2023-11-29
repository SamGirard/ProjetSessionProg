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

        private void btModifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void statut_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if (textBlock != null)
            {
                // Accéder à l'Ellipse par son nom
                Ellipse ellipse = FindName("couleur") as Ellipse;

                if (ellipse != null)
                {
                    // Changer la couleur de l'Ellipse en fonction du Statut
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
