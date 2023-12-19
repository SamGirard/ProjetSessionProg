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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetSession
{
    public sealed partial class AjoutEmployeProjet : ContentDialog
    {
        public AjoutEmployeProjet()
        {
            this.InitializeComponent();
            cbxEmploye.ItemsSource = Singleton.GetInstance().GetListeEmploye();
        }

        public string IdProjet { get; set; }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var erreur = false;

            string idProjet;
            string matricule;

            if (cbxEmploye.SelectedIndex == -1)
            {
                erreur = true;
                errNom.Text = "Veuillez choisir un employé";
                args.Cancel = true;
            }
            else errNom.Text = "";

            if(erreur == false)
            {
                Employe employeSelectionne = (Employe)cbxEmploye.SelectedItem;

                // Récupérez le matricule à partir de l'objet Employe
                matricule = employeSelectionne.Matricule;

                idProjet = tblIdProjet.Text;

                Singleton.GetInstance().ajouterEmployeProjet(idProjet, matricule);
            }
        }

    }
}
