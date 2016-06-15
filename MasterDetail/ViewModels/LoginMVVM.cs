using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metier;
using Library;
using System.Windows;
using MasterDetail.Events;
using System.Xml.Serialization;
using System.IO;

namespace MasterDetail.ViewModels {
    public class LoginMVVM : VMBase {

        #region View
        public LoginWindow Login { get; set; }
        public SignupWindow Signup { get; set; }

        #endregion

        #region Delegates
        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand SignupCommand { get; set; }

        #endregion

        #region ProprietesUtilisateur
        public List<Utilisateur> ListUtilisateur { get; set; }

        private Utilisateur _utilisateur;

        public Utilisateur Utilisateur
        {
            get { return _utilisateur; }
            set { _utilisateur = value; NotifyPropertyChanged("Utilisateur"); }
        }

        #endregion

        #region Constructeur
        public LoginMVVM(LoginWindow loginWindow)
        {
            Login = loginWindow;

            XmlSerializer xs = new XmlSerializer(typeof(List<Utilisateur>));

            StreamReader rd = new StreamReader("Xml/listeUtilisateur.xml");

            ListUtilisateur = xs.Deserialize(rd) as List<Utilisateur>;

            /*ListUtilisateur = new List<Utilisateur>();

            ListUtilisateur.Add(new Utilisateur("e", "e", "Vendeur", "Bahaki", "Eissam", "20", "Images/image.png", "eissam@live.fr"));
            ListUtilisateur.Add(new Utilisateur("o", "o", "Client", "Sevik", "Ozcan", "18", "Images/image.png", "ozcan@live.fr"));*/

            LoginCommand = new DelegateCommand(OnLoginCommand, CanExecuteLogin);
            SignupCommand = new DelegateCommand(OnSignupCommand, CanExecuteSignup);

            Utilisateur = new Utilisateur("", "", "", "", "", "", "Images/image.png","");
        }

        #endregion

        #region Evenements

        #region Signup
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

        #endregion

        #region Login
        private void OnLoginCommand(object o)
        {

            foreach (Utilisateur u in ListUtilisateur)
            {
                if (Utilisateur.Equals(u))
                {
                    Utilisateur = u;

                    MainWindow Mainwindow = new MainWindow(this);
                    Login.Hide();
                    Mainwindow.ShowDialog();
                    
                   Utilisateur = new Utilisateur("", "", "", "", "", "", "Images/image.png", "");

                    return;
                }
            }
            MessageBox.Show("Utilisateur Incorrect");
            Utilisateur = new Utilisateur("", "", "","","","", "Images/image.png","");
        }

        private bool CanExecuteLogin(object o)
        {
            return true;
        }

        #endregion

        #endregion
    }
}
