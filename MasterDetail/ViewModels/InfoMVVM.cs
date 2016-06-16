using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metier;
using MasterDetail.Events;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.IO;
using System.Windows;
using System.Device.Location;

namespace MasterDetail.ViewModels {
    public class InfoMVVM :VMBase{

        public bool ClickOnFin { get; set; }

        private string _texteItem;
        public string TexteItem
        {
            get
            {
                return _texteItem;
            }
            set
            {
                _texteItem = value;
                NotifyPropertyChanged("TexteItem");
            }
        }

        #region Delegates
        public DelegateCommand OpenCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand DelCommand { get; set; }
        public LoginMVVM LoginVM { get; set; }

        #endregion

        #region  ProprietesUtilisateur

        private Utilisateur _selectedUtilisateur;
        public Utilisateur SelectedUtilisateur
        {
            get
            {
                return _selectedUtilisateur;
            }

            set
            {
                _selectedUtilisateur = value;
                NotifyPropertyChanged("SelectedUtilisateur");
            }
        }

        private ObservableCollection<Utilisateur> _listeAdapteAuType;

        public ObservableCollection<Utilisateur> ListeAdapteAuType
        {
            get
            {
                return _listeAdapteAuType;
            }

            set
            {
                _listeAdapteAuType = value;
                NotifyPropertyChanged("ListeAdapteAuType");
            }
        }

        #endregion

        #region Constructeur
        public InfoMVVM(LoginMVVM loginVM)
        {
            LoginVM = loginVM;

            OpenCommand = new DelegateCommand(OnOpenCommand, CanExecuteOpen);
            SaveCommand = new DelegateCommand(OnSaveCommand, CanExecuteSave);
            DelCommand = new DelegateCommand(OnDelCommand, CanExecuteDel);

            ClickOnFin = false;

            if (LoginVM.Utilisateur.Type.Equals("Vendeur"))
            {
                TexteItem = "Clients";
            }
            else
            {
                TexteItem = "Vendeurs";
            }

            ListeAdapteAuType = new ObservableCollection<Utilisateur>();
            RemplissageListe(LoginVM.Utilisateur.Type);
        }

        #endregion

        #region Methodes
        public void RemplissageListe(string type)
        {
            foreach (Utilisateur u in LoginVM.ListUtilisateur)
                if (!u.Type.Equals(type))
                    ListeAdapteAuType.Add(u);
        }

        #endregion

        #region Evenements

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
                LoginVM.Utilisateur.ImagePath = op.FileName;
            }
        }

        private bool CanExecuteOpen(object obj)
        {
            return true;
        }

        #endregion

        #region Save
     
        private void OnSaveCommand(object obj)
        {
            ClickOnFin = true;
            MessageBox.Show("Informations enregistrées");
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);

            
        }
        private bool CanExecuteSave(object obj)
        {
            return true;
        }

        #endregion

        #region Del
        private void OnDelCommand(object obj)
        {
            ClickOnFin = true;
            LoginVM.ListUtilisateur.Remove(LoginVM.Utilisateur);
            MessageBox.Show("Utilisateur Supprimé");
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }
        private bool CanExecuteDel(object obj)
        {
            return true;
        }

        #endregion

        #endregion
    }

}
