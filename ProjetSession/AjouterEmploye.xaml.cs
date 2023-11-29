﻿using Microsoft.UI.Xaml;
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

        string nom = "";
        string prenom = "";
        DateTime dateNaissance;
        string email = "";
        string adresse = "";
        DateTime dateEmbauche;
        string photo = "";
        string statut = "";


        public AjouterEmploye()
        {
            this.InitializeComponent();
            cbxProjet.ItemsSource = Singleton.GetInstance().GetNomsProjets();
            cdpEmb.MaxDate = DateTime.Now;
            cdpEmb.MinDate = new DateTime(1980, 01, 01);

            cdpNaiss.MaxDate = new DateTime(2003, 01, 01);
            cdpNaiss.MinDate = new DateTime(1950, 01, 01);
        }


        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var erreur = false;

            /////////////////////NOM\\\\\\\\\\\\\\\\\\\\
            if (tbxNom.Text == "")
                {
                    errNom.Text = "Le nom ne peut pas être vide";
                    erreur = true;
                    args.Cancel = true;
                }
            else 
                {
                    errNom.Text = "";
                    nom = tbxNom.Text;
                }

            ////////////////////PRÉNOM\\\\\\\\\\\\\\\\\\\\
            if (tbxPrenom.Text == "")
            {
                errPrenom.Text = "Le prénom ne peut pas être vide";
                erreur = true;
                args.Cancel = true;
            }
            else
            {
                errPrenom.Text = "";
                prenom = tbxPrenom.Text;
            }

            ////////////////////EMAIL\\\\\\\\\\\\\\\\\\\\
            if (tbxEmail.Text == "")
            {
                errEmail.Text = "L'email ne peut pas être vide";
                erreur = true;
                args.Cancel = true;
            }
            else
            {
                errEmail.Text = "";
                email = tbxEmail.Text;
            }

            ////////////////////ADRESSE\\\\\\\\\\\\\\\\\\\\
            if (tbxAdresse.Text == "")
            {
                errAdresse.Text = "L'adresse ne peut pas être vide";
                erreur = true;
                args.Cancel = true;
            }
            else
            {
                errAdresse.Text = "";
                adresse = tbxAdresse.Text;
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
                if (int.TryParse(tbxTaux.Text, out int result))
                {
                    if (Convert.ToInt32(tbxTaux.Text) < 0)
                    {
                        erreur = true;
                        errTaux.Text = "Le taux ne peut pas être négatif";
                    }
                    else if(Convert.ToInt32(tbxTaux.Text) < 15)
                    {
                        erreur = true;
                        errTaux.Text = "Le taux ne peut pas être inférieur au salair minimum";
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
                /*if(cdpNaiss.Date.Value.Year < 1950)
                {
                    erreur = true;
                    errDate.Text = "La date de naissance ";
                }*/
                dateNaissance = cdpNaiss.Date.Value.Date;
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
                dateEmbauche = cdpEmb.Date.Value.Date;
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
                    photo = tbxPhoto.Text;
                }
            }

            ////////////////////STATUT\\\\\\\\\\\\\\\\\\\\
            if (cbxStatut.SelectedIndex == -1)
            {
                errStatut.Text = "Le statut ne peut pas être vide";
                erreur = true;
                args.Cancel = true;
            }
            else
            {
                errStatut.Text = "";
                statut = cbxStatut.SelectedItem.ToString();
            }


            if (erreur == false)
            {
                double dTaux = Convert.ToDouble(tbxTaux.Text);
                string projet;
                if (cbxProjet.SelectedIndex == -1)
                {
                    projet = null;
                }
                else
                {
                    projet = cbxProjet.SelectedItem.ToString();
                }

                Singleton.GetInstance().AjouterEmploye(nom, prenom, dateNaissance, email, adresse, dateEmbauche, dTaux, photo, projet, statut);
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

        /*
         DateTime selectedDate = calendarDatePicker1.Date.DateTime;
         string formattedDate = selectedDate.ToString("yyyy-MM-dd");
         */
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

        public int Statut
        {
            get { return cbxStatut.SelectedIndex; }
            set { cbxStatut.SelectedIndex = value;}
        }

    }
}