using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
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

            ObservableCollection<Projet> projets = Singleton.GetInstance().GetListeProjet(); // Assurez-vous de remplacer cela par votre logique de chargement
            List<Projet> viewModels = projets.Select(p => new Projet { Statut = p.Statut }).ToList();
            gvListe.ItemsSource = projets;
        }
        private void gvListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Projet unProjet = gvListe.SelectedItem as Projet;

            this.Frame.Navigate(typeof(PageZoomProjet), unProjet);

        }

        /*
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
            dialog.DateDebut = projet.DateDebutTest.ToString();
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
         //   }
     //   }

/*
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

          //  }
            
       // }

    }
}
