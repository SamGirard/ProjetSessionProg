using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlX.XDevAPI.Common;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetSession
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageZoomProjet : Page
    {
        public PageZoomProjet()
        {
            this.InitializeComponent();

            Style itemContainerStyle = new Style(typeof(ListViewItem));
            itemContainerStyle.Setters.Add(new Setter(FontSizeProperty, 25.0));
            itemContainerStyle.Setters.Add(new Setter(MarginProperty, new Thickness(0)));
            itemContainerStyle.Setters.Add(new Setter(PaddingProperty, new Thickness(20)));
            lvListe.ItemContainerStyle = itemContainerStyle;
        }

        Projet projetModif;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                Projet unProjet = e.Parameter as Projet;
                projetModif = unProjet;
                tblTitre.Text = unProjet.Titre;
                tblBudget.Text = unProjet.BudgetString;
                tblDate.Text = unProjet.DateDebutTest.ToString();
                tblDesc.Text = unProjet.Description;
                tblIdClient.Text = unProjet.IdCLient;
                tblIdProjet.Text = unProjet.IdProjet;
                tblNbEmp.Text = unProjet.NbEmploye.ToString();
                tblStatut.Text = unProjet.Statut;
                tblSalaire.Text = unProjet.TotalSalString;
                lvListe.ItemsSource = unProjet.ListeEmploye;
                rond.Fill = unProjet.EllipseColor;
            }
        }
        private async void btModifier_Click(object sender, RoutedEventArgs e)
        {
            Projet projet = projetModif;
            AjoutProjetContent dialog = new AjoutProjetContent();
            dialog.XamlRoot = modifFrame.XamlRoot;
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
            }
        }
    }
}
