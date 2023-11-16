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
    public sealed partial class AjoutClientContent : ContentDialog
    {
        public AjoutClientContent()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Boolean erreur = false;
            string champVide = "Ce champs ne peut pas être vide";

            if (string.IsNullOrEmpty(tbxNom.Text))
            {
                erreur = true;
                errNom.Text = "Le champ nom est obligatoire";
            }
            else
            {
                errNom.Text = "";
            }

            if (string.IsNullOrEmpty(tbxEmail.Text))
            {
                erreur = true;
                errEmail.Text = "Le champ email est obligatoire";
            }
            else
            {
                errEmail.Text = "";
            }

            if (string.IsNullOrEmpty(tbxAdresse.Text))
            {
                erreur = true;
                errAdresse.Text = "Le champ adresse est obligatoire";
            }
            else
            {
                errAdresse.Text = "";
            }

            if (string.IsNullOrEmpty(tbxTel.Text))
            {
                erreur = true;
                errTel.Text = "Le champ numéro de téléphone est obligatoire";
            }
            else
            {
                errTel.Text = "";
            }

        }
    }
}
