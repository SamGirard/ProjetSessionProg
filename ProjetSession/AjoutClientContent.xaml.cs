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

            if(erreur == false)
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

        }

        /*private void tbxTel_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = tbxTel.Text;

            string digitsOnly = new string(input.Where(char.IsDigit).ToArray());


            if (digitsOnly.Length == 3)
            {
                digitsOnly = $"({input})-";
            }

            if (digitsOnly.Length == 9)
            {
                digitsOnly = $"{input.Substring(0, 6)}{input.Substring(7, 9)}-";
            }


            // Mettre à jour le texte dans le TextBox
            tbxTel.Text = digitsOnly;
            tbxTel.SelectionStart = digitsOnly.Length;
        }*/
    }
}
