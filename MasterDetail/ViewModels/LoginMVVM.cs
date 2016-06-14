using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metier;
using Library;
using System.Windows;
using MasterDetail.Events;

namespace MasterDetail.ViewModels {
    public class LoginMVVM : VMBase 
    {
        public SignupWindow Signup { get; set; }
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand SignupCommand { get; set; }
        public List<Utilisateur> ListUtilisateur { get; set; }

        private Utilisateur _utilisateur;

        public Utilisateur Utilisateur
        {
            get { return _utilisateur; }
            set { _utilisateur = value; NotifyPropertyChanged("Utilisateur"); }
        }


        public LoginMVVM()
        {
            

            ListUtilisateur = new List<Utilisateur>();
            ListUtilisateur.Add(new Utilisateur("e", "e", "Vendeur", "Bahaki", "Eissam", "20", "C:\\Users\\ozcan\\Documents\\COURS\\C# .NET  WPF XAML\\TP IHM\\MasterDetail\\Images\\Icons\\1.png","eissam@live.fr"));
            ListUtilisateur.Add(new Utilisateur("o", "o", "Client", "Sevik", "Ozcan", "18", "C:\\Users\\ozcan\\Documents\\COURS\\C# .NET  WPF XAML\\TP IHM\\MasterDetail\\Images\\Icons\\9.png", "ozcan@live.fr"));

            LoginCommand = new DelegateCommand(OnLoginCommand, CanExecuteLogin);
            SignupCommand = new DelegateCommand(OnSignupCommand, CanExecuteSignup);

            Utilisateur = new Utilisateur("", "", "", "", "", "", "C:\\Users\\ozcan\\Documents\\COURS\\C# .NET  WPF XAML\\TP IHM\\MasterDetail\\Images\\image.png","");
        }

        private void OnSignupCommand(object obj)
        {
            Signup = new SignupWindow();
            ButtonPressedEvent.GetEvent().Handler += CloseSignupWindow;
            Signup.ShowDialog();

            if (Signup.SignupVM.ClickOnSignup)
                ListUtilisateur.Add(Signup.SignupVM.NewUtilisateur);
        }

        private void CloseSignupWindow(object sender, EventArgs e)
        {
            Signup.Close();

            ButtonPressedEvent.GetEvent().Handler -= CloseSignupWindow;
        }
        private bool CanExecuteSignup(object obj)
        {
            return true;
        }

        private void OnLoginCommand(object o)
        {
            foreach (Utilisateur u in ListUtilisateur)
            {
                if (Utilisateur.Equals(u))
                {
                    Utilisateur = u;

                    MainWindow mainwindow = new MainWindow(this);
                    mainwindow.ShowDialog();
                    Utilisateur = new Utilisateur("", "", "", "", "", "", "C:\\Users\\ozcan\\Documents\\COURS\\C# .NET  WPF XAML\\TP IHM\\MasterDetail\\Images\\image.png", "");

                    return;
                }
            }
            MessageBox.Show("Utilisateur Incorrect");
            Utilisateur = new Utilisateur("", "", "","","","", "C:\\Users\\ozcan\\Documents\\COURS\\C# .NET  WPF XAML\\TP IHM\\MasterDetail\\Images\\image.png","");
        }

        private bool CanExecuteLogin(object o)
        {
            return true;
        }

    }
}
