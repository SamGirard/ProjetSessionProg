using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
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
    public sealed partial class PageAfficherProjet : Page
    {
        public PageAfficherProjet()
        {
            this.InitializeComponent();
            gvListe.ItemsSource = Singleton.GetInstance().GetListeProjet();

            ObservableCollection<Projet> projets = Singleton.GetInstance().GetListeProjet();
            List<Projet> viewModels = projets.Select(p => new Projet { Statut = p.Statut }).ToList();
            gvListe.ItemsSource = projets;
        }
        private void gvListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Projet unProjet = gvListe.SelectedItem as Projet;

            this.Frame.Navigate(typeof(PageZoomProjet), unProjet, new DrillInNavigationTransitionInfo());
        }

        

    }
}
