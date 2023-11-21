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
    public sealed partial class connexion : ContentDialog
    {
        string user;
        string mdp;

        public connexion()
        {
            this.InitializeComponent();
        }

        public string User { get => user; }
        public string Mdp { get => mdp; }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            user = tbUtilisateur.Text;
            mdp = tbMotDePasse.Password;
            bool infoValide = Singleton.GetInstance().verif_Admin(user, mdp);

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(mdp))
            {
                err.Text = "Les données entrées sont invalide";
                args.Cancel = true;
            }
            else if(infoValide == false) {
                err.Text = "Les données entrées sont invalide";
                args.Cancel = true;
            }
            else
            {
                args.Cancel = false;
            }
        }
    }
}
