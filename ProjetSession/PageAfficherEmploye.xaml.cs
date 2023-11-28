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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetSession
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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

        private void btModifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
