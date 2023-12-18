using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace ProjetSession
{
    public sealed partial class AjouterEmploye : ContentDialog
    {
        Boolean modifier = false;
        public AjouterEmploye()
        {
            this.InitializeComponent();
            cbxProjet.ItemsSource = Singleton.GetInstance().GetListeProjet();

            cdpEmb.MaxDate = DateTime.Now;
            cdpEmb.MinDate = new DateTime(1980, 01, 01);

            cdpNaiss.MaxDate = new DateTime(2005, 01, 01);
            cdpNaiss.MinDate = new DateTime(1950, 01, 01);
        }

        public CalendarDatePicker CdpNaiss
        {
            get { return cdpNaiss; }
        }

        public CalendarDatePicker CdpEmb
        {
            get { return cdpEmb; }
        }


        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var erreur = false;
            Projet projet;

            /////////////////////PROJET\\\\\\\\\\\\\\\\\\\\
            if (cbxProjet.SelectedIndex < 0)
            {
                projet = null;
            }
            else
            {
                projet = cbxProjet.SelectedItem as Projet;
            }

            /////////////////////NOM\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(tbxNom.Text))
            {
                    errNom.Text = "Le nom ne peut pas être vide";
                    erreur = true;
                    args.Cancel = true;
            }
            else 
            {
                    errNom.Text = "";
            }

            ////////////////////PRÉNOM\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(tbxPrenom.Text))
            {
                errPrenom.Text = "Le prénom ne peut pas être vide";
                erreur = true;
                args.Cancel = true;
            }
            else
            {
                errPrenom.Text = "";
            }

            ////////////////////EMAIL\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(tbxEmail.Text))
            {
                errEmail.Text = "L'email ne peut pas être vide";
                erreur = true;
                args.Cancel = true;
            }
            else
            {
                errEmail.Text = "";
            }

            ////////////////////ADRESSE\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(tbxAdresse.Text))
            {
                errAdresse.Text = "L'adresse ne peut pas être vide";
                erreur = true;
                args.Cancel = true;
            }
            else
            {
                errAdresse.Text = "";
            }

            ////////////////////TAUX\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(tbxTaux.Text))
            {
                errTaux.Text = "Le taux ne peut pas être vide";
                erreur = true;
                args.Cancel = true;
            }
            else
            {
                if (double.TryParse(tbxTaux.Text, out double result))
                {
                    if (Convert.ToDouble(tbxTaux.Text) < 0)
                    {
                        erreur = true;
                        errTaux.Text = "Le taux ne peut pas être négatif";
                    }
                    else if(Convert.ToDouble(tbxTaux.Text) < 15)
                    {
                        erreur = true;
                        errTaux.Text = "Le taux ne peut pas être inférieur au salair minimum (15$/h)";
                    }
                    else
                    {
                        errTaux.Text = "";
                    }
                }
                else
                {
                    erreur = true;
                    errTaux.Text = "Le taux doit être un nombre";
                }
            }

            ////////////////////DATE NAISSANCE\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(cdpNaiss.Date.ToString()))
            {
                erreur = true;
                errDate.Text = "La date de naissance ne peut pas être vide";
                args.Cancel = true;
            }
            else
            {
                errDate.Text = "";
            }

            ////////////////////DATE EMBAUCHE\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(cdpEmb.Date.ToString()))
            {
                erreur = true;
                errEmbauche.Text = "La date d'embauche ne peut pas être vide";
                args.Cancel = true;
            }
            else
            {
                errEmbauche.Text = "";
            }

            ////////////////////PHOTO\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(tbxPhoto.Text))
            {
                errPhoto.Text = "La photo ne peut pas être vide";
                erreur = true;
                args.Cancel = true;
            }
            else
            {
                if (UrlValide(tbxPhoto.Text) == false)
                {
                    errPhoto.Text = "Le lien URL est invalide";
                    erreur = true;
                    args.Cancel = true;
                }
                else
                {
                    errPhoto.Text = "";
                }
            }

            if(cbxProjet.SelectedIndex == -1)
            {
                erreur = true;
                errProjet.Text = "L'employé doit avoir un projet";
                args.Cancel = true;
            }
            else
            {
                errProjet.Text = "";
            }


            /////////////////////**AJOUT**\\\\\\\\\\\\\\\\\\\\
            if (erreur == false)
            {
                Employe employe = new Employe 
                {
                    Nom = tbxNom.Text,
                    Prenom = tbxPrenom.Text,
                    DateNaiss = cdpNaiss.Date.Value.ToString("yyyy-MM-dd"),
                    Email = tbxEmail.Text,
                    Adresse = tbxAdresse.Text,
                    DateEmb = cdpEmb.Date.Value.ToString("yyyy-MM-dd"),
                    Photo = tbxPhoto.Text,
                    Statut = null,
                    TauxHor = Convert.ToDouble(tbxTaux.Text),
                    ProjetEnCours = projet,
                    IdProjet = projet.IdProjet
                };
                Singleton.GetInstance().ajouter(employe);
            }
        }

        /*********************VALIDATION URL*********************/
        private bool UrlValide(string uriString)
        {
            System.Uri uri;
            return System.Uri.TryCreate(uriString, UriKind.Absolute, out uri)
            && (uri.Scheme == System.Uri.UriSchemeHttp || uri.Scheme == System.Uri.UriSchemeHttps);
        }


        /*********************PARTIE MODIFICATION*********************/
        string matricule;
        public string Matricule
        {
            get { return matricule; }
            set { matricule = value; }
        }

        public string Nom
        {
            get { return tbxNom.Text; }
            set { tbxNom.Text = value; }
        }

        public string Prenom
        {
            get { return tbxPrenom.Text; }
            set { tbxPrenom.Text = value; }
        }

        public string Date_Naissance
        {
            get { return cdpNaiss.Date.ToString(); }
            set 
            {
                DateTimeOffset date = new DateTimeOffset(DateTime.Parse(value));
                cdpNaiss.Date = date; 
            }
        }

        public string Email 
        { 
            get { return tbxEmail.Text; } 
            set { tbxEmail.Text = value;}
        }

        public string Adresse
        {
            get { return tbxAdresse.Text;}
            set { tbxAdresse.Text = value;}
        }

        public string Date_Embauche
        {
            get { return cdpEmb.Date.ToString(); }
            set
            {
                DateTimeOffset date = new DateTimeOffset(DateTime.Parse(value));
                cdpEmb.Date = date;
            }
        }

        public string Taux
        {
            get { return tbxTaux.Text; }
            set { tbxTaux.Text = value;}
        }

        public string Photo
        {
            get { return tbxPhoto.Text; }
            set { tbxPhoto.Text = value; }
        }
        
        public int IdProjet
        {
            get { return cbxProjet.SelectedIndex; }
            set {  cbxProjet.SelectedIndex = value;}
        }
    }
}