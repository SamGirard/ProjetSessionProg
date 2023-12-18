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

            cdpDate.MaxDate = DateTime.Now;
            cdpDate.MinDate = new DateTime(1980, 01, 01);
        }
        

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var erreur = false;
            Client client = cbClient.SelectedItem as Client;

            ////////////////////TITRE\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(tbTitre.Text))
            {
                erreur = true;
                errTitre.Text = "Le titre ne peut pas être vide";
                args.Cancel = true;
            }
            else
            {
                errTitre.Text = "";
            }

            ////////////////////DATE DÉBUT\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(cdpDate.Date.ToString()))
            {
                erreur = true;
                errDate.Text = "La date ne peut pas être vide";
                args.Cancel = true;
            }
            else
            {
                errDate.Text = "";
            }

            ////////////////////CLIENT\\\\\\\\\\\\\\\\\\\\
            if (cbClient.SelectedIndex == -1)
            {
                erreur = true;
                errClient.Text = "Le client ne peut pas être vide";
                args.Cancel = true;
            }
            else
            {
                errClient.Text = "";
            }

            ////////////////////DESCRIPTION\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(tbDescription.Text))
            {
                erreur = true;
                errDesc.Text = "La description ne peut pas être vide";
                args.Cancel = true;
            }
            else
            {
                errDesc.Text = "";
            }

            ////////////////////BUDGET\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(tbBudget.Text))
            {
                erreur = true;
                errBudget.Text = "Le budget ne peut pas être vide";
                args.Cancel = true;
            }
            else
            {
                if (double.TryParse(tbBudget.Text, out double result))
                {
                    if (Convert.ToDouble(tbBudget.Text) < 0)
                    {
                        erreur = true;
                        errBudget.Text = "Le budget ne peut pas être négatif";
                    }
                    else
                    {
                        errBudget.Text = "";
                    }
                }
                else
                {
                    erreur = true;
                    errBudget.Text = "Le budget doit être un nombre";
                }
            }

            ////////////////////NB EMPLOYÉ\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(tbNbEmploye.Text))
            {
                erreur = true;
                errNbEmploye.Text = "Le nombre d'employé ne peut pas être vide";
                args.Cancel = true;
            }
            else
            {
                if (int.TryParse(tbNbEmploye.Text, out int result))
                {
                    if (Convert.ToInt32(tbNbEmploye.Text) <= 0)
                    {
                        erreur = true;
                        errNbEmploye.Text = "Le nombre d'employé doit être supérieur à 0";
                    }
                    else if(Convert.ToInt32(tbNbEmploye.Text) > 5)
                    {
                        erreur = true;
                        errNbEmploye.Text = "Le nombre d'employé ne peut pas dépasser 5";
                    }
                    else
                    {
                        errNbEmploye.Text = "";
                    }
                }
                else
                {
                    erreur = true;
                    errNbEmploye.Text = "Le nombre d'employé doit être un nombre";
                }
            }

            ////////////////////STATUT\\\\\\\\\\\\\\\\\\\\
            if (cbStatut.SelectedIndex == -1)
            {
                erreur = true;
                errStatut.Text = "Le statut ne peut pas être vide";
                args.Cancel = true;
            }
            else
            {
                errStatut.Text = "";
            }





            /////////////////////**AJOUT**\\\\\\\\\\\\\\\\\\\\
            if (erreur == false && modifier == 0)
            {
                Projet projet = new Projet
                {
                    Titre = tbTitre.Text,
                    DateDebut = cdpDate.Date.Value.ToString("yyyy-MM-dd"),
                    IdCLient = client.Id_Client,
                    Description = tbDescription.Text,
                    Budget = Convert.ToDouble(tbBudget.Text),
                    NbEmploye = Convert.ToInt32(tbNbEmploye.Text),
                    Statut = cbStatut.SelectedItem.ToString(),
                    Client = client
                };
                Singleton.GetInstance().ajouter(projet);
            }
            else if (modifier == 1)
            {
                Projet projet = new Projet
                {
                    IdProjet = idProjet,
                    Titre = tbTitre.Text,
                    DateDebut = cdpDate.Date.Value.ToString("yyyy-MM-dd"),
                    IdCLient = client.Id_Client,
                    Description = tbDescription.Text,
                    Budget = Convert.ToDouble(tbBudget.Text),
                    NbEmploye = Convert.ToInt32(tbNbEmploye.Text),
                    Statut = cbStatut.SelectedItem.ToString(),
                    Client = client
                };
                Singleton.GetInstance().modifier(projet);
            }
        }



        /*********************PARTIE MODIFICATION*********************/
        int modifier = 0;
        public int Modifier
        {
            get { return modifier; }
            set { modifier = value; }
        }

        string idProjet;
        public string IdProjet
        {
            get { return idProjet; }
            set { idProjet = value; }
        }

        public string Titre
        {
            get { return tbTitre.Text; }
            set { tbTitre.Text = value;}
        }

        public string DateDebut
        {
            get { return cdpDate.Date.ToString(); }
            set
            {
                DateTimeOffset date = new DateTimeOffset(DateTime.Parse(value));
                cdpDate.Date = date;
            }
        }

        public string Description
        {
            get { return tbDescription.Text; }
            set { tbDescription.Text = value; }
        }

        public string Budget
        {
            get { return tbBudget.Text; }
            set { tbBudget.Text = value; }
        }

        public string NbEmploye
        {
            get { return tbNbEmploye.Text; }
            set { tbNbEmploye.Text = value;}
        }

        public int IdClient
        {
            get { return cbClient.SelectedIndex; }
            set { cbClient.SelectedIndex = value; }
        }

        public int Statut
        {
            get { return cbStatut.SelectedIndex; }
            set { cbStatut.SelectedIndex = value; }
        }
    }
}
