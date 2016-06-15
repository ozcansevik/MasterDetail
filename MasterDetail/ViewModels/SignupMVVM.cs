using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Metier;
using MasterDetail.Events;
using System.Windows;

namespace MasterDetail.ViewModels {
    public class SignupMVVM : VMBase {

        public bool ClickOnSignup { get; set; }

        private Utilisateur _newUtilisateur;

        public Utilisateur NewUtilisateur
        {
            get
            {
                return _newUtilisateur;
            }

            set
            {
                _newUtilisateur = value;
                NotifyPropertyChanged("NewUtilisateur");
            }
        }

        #region Delegates
        public DelegateCommand SignupCommand { get; set; }

        #endregion

        #region Constructeur
        public SignupMVVM()
        {
            ClickOnSignup = false;
            SignupCommand = new DelegateCommand(OnSignupCommand, CanExecuteSignup);
            NewUtilisateur = new Utilisateur("", "", "", "", "", "", "C:\\Users\\ozcan\\Documents\\COURS\\C# .NET  WPF XAML\\TP IHM\\MasterDetail\\Images\\image.png","");
        }

        #endregion

        #region Evenements

        #region Signup
        private void OnSignupCommand(object obj)
        {
            MessageBox.Show("Inscription effectué vous pouvez vous connectez");
            ClickOnSignup = true;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }
        private bool CanExecuteSignup(object obj)
        {
            return true;
        }

        #endregion

        #endregion
    }
}
