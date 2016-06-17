using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Metier;
using MasterDetail.Events;
using System.Windows;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Win32;

namespace MasterDetail.ViewModels {
    public class SignupMVVM : VMBase {

        private bool _isEnabled;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }

            set
            {
                _isEnabled = value;
                NotifyPropertyChanged("IsEnabled");
            }
        }

        public bool ClickOnSignup { get; set; }
        public SignupWindow Signup { get; set; }
        
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
        private DelegateCommand _signupCommand;
        public DelegateCommand SignupCommand
        {
            get
            {
                return _signupCommand;
            }

            set
            {
                _signupCommand = value;
                
            }
        }
        public DelegateCommand OpenCommand { get; set; }


        #endregion

        #region Constructeur
        public SignupMVVM(SignupWindow signup)
        {
            Signup = signup;
            ClickOnSignup = false;
            IsEnabled = false;

            SignupCommand = new DelegateCommand(OnSignupCommand, CanExecuteSignup);
            OpenCommand = new DelegateCommand(OnOpenCommand, CanExecuteOpen);

            NewUtilisateur = new Utilisateur("", "", "", "", "", "", "Images/image.png","", new Location(0,0));
        }

        #endregion

        #region Evenements

        #region Signup
        private void OnSignupCommand(object obj)
        {
            NewUtilisateur.Position = Signup.PositionPin;

            var existingCount = Signup.Login.ListUtilisateur.Count(p =>
                p.UserName == NewUtilisateur.UserName);

            if (NewUtilisateur.UserName == String.Empty || NewUtilisateur.Password == String.Empty || NewUtilisateur.Type == String.Empty) {

                MessageBox.Show("Veuillez remplir tous les champs requis");
                return;
            }

            if (existingCount > 0)
            {
                MessageBox.Show("Nom d'utilisateur déja utilisé");
            }

            else
            {
                MessageBox.Show("Inscription effectuée vous pouvez vous connectez");
                ClickOnSignup = true;
                ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
            }

                
        }
        private bool CanExecuteSignup(object obj)
        {
            return true;
        }

        #endregion

        #region Open

        private void OnOpenCommand(object obj)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Selectionner une image";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                NewUtilisateur.ImagePath = op.FileName;
            }
        }
        private bool CanExecuteOpen(object obj)
        {
            return true;
        }

        #endregion

        #endregion
    }
}
