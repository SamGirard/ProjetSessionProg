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

namespace ProjetSession
{
    public sealed partial class AjoutProjetContent : ContentDialog
    {
        public AjoutProjetContent()
        {
            this.InitializeComponent();
            cbClient.ItemsSource = Singleton.GetInstance().GetListeClient();
        }

        string titre = "";
        DateTime dateDebut;
        string client = "";
        string description = "";
        int budget;
        int nbEmploye;
        string status = "";
        

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var erreur = false;

            if (tbTitre.Text == "")
            {
                erreur = true;
                errTitre.Text = "Le titre est vide";
                args.Cancel = true;
            }
            else
            {
                tbTitre.Text = titre;
                errTitre.Text = "";
            }
            


            if (cdpDate.Date == DateTimeOffset.MinValue)
            {
                erreur = true;
                errDate.Text = "La date est vide";
                args.Cancel = true;
            }
            else
            {
                cdpDate.Date = dateDebut;
                errDate.Text = "";
            }


            if (cbClient.SelectedIndex == -1)
            {
                erreur = true;
                errClient.Text = "Le client est vide";
                args.Cancel = true;
            }
            else
            {
                cbClient.SelectedItem = client;
                errClient.Text = "";
            }


            if (tbDescription.Text == "")
            {
                erreur = true;
                errDesc.Text = "La description est vide";
                args.Cancel = true;
            }
            else
            {
                tbDescription.Text = description;
                errDesc.Text = "";
            }


            if (tbBudget.Text == "")
            {
                erreur = true;
                errBudget.Text = "Le budget est vide";
                args.Cancel = true;
            }
            else
            {
                budget = Convert.ToInt32(tbBudget.Text);
                errBudget.Text = "";
            }


            if (tbNbEmploye.Text == "")
            {
                erreur = true;
                errNbEmploye.Text = "Le nombre d'employé est vide";
                args.Cancel = true;
            }
            else
            {
                budget = Convert.ToInt32(tbNbEmploye.Text);
                errNbEmploye.Text = "";
            }


            if (cbStatut.SelectedIndex == -1)
            {
                erreur = true;
                errStatut.Text = "Le status est vide";
                args.Cancel = true;
            }
            else
            {
                cbStatut.SelectedItem = status;
                errStatut.Text = "";
            }



            if (erreur == false)
            {
                Singleton.GetInstance().AjouterProjet(titre, dateDebut, client, description, budget, nbEmploye, status);
            }
        }
    }
}
