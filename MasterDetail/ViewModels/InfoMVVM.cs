using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metier;
using MasterDetail.Events;
using System.Collections.ObjectModel;

namespace MasterDetail.ViewModels {
    public class InfoMVVM :VMBase{

        public DelegateCommand FinCommand { get; set; }
        public DelegateCommand DelCommand { get; set; }
        public LoginMVVM LoginVM { get; set; }
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

        public InfoMVVM(LoginMVVM loginVM)
        {
            LoginVM = loginVM;
            FinCommand = new DelegateCommand(OnFinCommand, CanExecuteFin);
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

        public void RemplissageListe(string type)
        {
            foreach (Utilisateur u in LoginVM.ListUtilisateur)
                if (!u.Type.Equals(type))
                    ListeAdapteAuType.Add(u);
        }

        private void OnFinCommand(object obj)
        {
            ClickOnFin = true;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }
        private bool CanExecuteFin(object obj)
        {
            return true;
        }

        private void OnDelCommand(object obj)
        {
            ClickOnFin = true;
            LoginVM.ListUtilisateur.Remove(LoginVM.Utilisateur);
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }
        private bool CanExecuteDel(object obj)
        {
            return true;
        }

    }

}
