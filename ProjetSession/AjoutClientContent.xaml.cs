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
using Windows.Media.AppBroadcasting;

namespace ProjetSession
{
    public sealed partial class AjoutClientContent : ContentDialog
    {
        public AjoutClientContent()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Boolean erreur = false;

            /////////////////////Nom\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(tbxNom.Text))
            {
                erreur = true;
                errNom.Text = "Le champ nom est obligatoire";
                args.Cancel = true;
            }
            else
            {
                errNom.Text = "";
            }

            /////////////////////EMAIL\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(tbxEmail.Text))
            {
                erreur = true;
                errEmail.Text = "Le champ email est obligatoire";
                args.Cancel = true;
            }
            else
            {
                errEmail.Text = "";
            }

            /////////////////////ADRESSE\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(tbxAdresse.Text))
            {
                erreur = true;
                errAdresse.Text = "Le champ adresse est obligatoire";
                args.Cancel = true;
            }
            else
            {
                errAdresse.Text = "";
            }

            /////////////////////NUMÉRO DE TÉLÉPHONE\\\\\\\\\\\\\\\\\\\\
            if (string.IsNullOrEmpty(tbxTel.Text))
            {
                erreur = true;
                errTel.Text = "Le champ numéro de téléphone est obligatoire";
                args.Cancel = true;
            }
            else
            {
                errTel.Text = "";
            }


            /////////////////////**AJOUT**\\\\\\\\\\\\\\\\\\\\
            if (erreur == false && modifier == 0)
            {
                Client client = new Client 
                { 
                    Nom = tbxNom.Text,
                    Adresse = tbxAdresse.Text,
                    Num_Tel = tbxTel.Text,
                    Email = tbxEmail.Text
                };
                Singleton.GetInstance().ajouter(client);
            }
            else if(modifier == 1)
            {
                Client client = new Client
                {
                    Id_Client = idClient,
                    Nom = tbxNom.Text,
                    Adresse = tbxAdresse.Text,
                    Num_Tel = tbxTel.Text,
                    Email = tbxEmail.Text
                };
                Singleton.GetInstance().modifier(client);
            }

        }



        /*********************PARTIE MODIFICATION*********************/
        int modifier = 0;
        public int Modifier
        {
            get { return modifier; }
            set { modifier = value; }
        }

        string idClient;
        public string IdClient
        {
            get { return idClient; }
            set { idClient = value; }
        }

        public string Nom
        {
            get { return tbxNom.Text; }
            set { tbxNom.Text = value;}
        }

        public string Adresse
        {
            get { return tbxAdresse.Text; }
            set { tbxAdresse.Text = value; }
        }

        public string Num_Tel
        {
            get { return tbxTel.Text; }
            set { tbxTel.Text = value; }
        }

        public string Email
        {
            get { return tbxEmail.Text;}
            set { tbxEmail.Text = value;}
        }

    }
}
