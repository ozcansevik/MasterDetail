using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using Library;
using Metier;
using MasterDetail.Events;

namespace MasterDetail.ViewModels
{

    public class MainMVVM : VMBase
    {

        public AddView Add { get; set; }

        private string _textEdit;

        public string TextEdit
        {
            get
            {
                return _textEdit;
            }
            set
            {
                _textEdit = value;
                NotifyPropertyChanged("TextEdit");
            }
        }
    
        #region Bool

       

        private bool _isEnabledAfterEdit;
        public bool IsEnabledAfterEdit
        {
            get
            {
                return _isEnabledAfterEdit;
            }

            set
            {
                _isEnabledAfterEdit = value;
                NotifyPropertyChanged("IsEnabledAfterEdit");
            }
        }

        private bool _isEnabledButton;
        public bool IsEnabledButton
        {
            get
            {
                return _isEnabledButton;
            }

            set
            {
                _isEnabledButton = value;
                NotifyPropertyChanged("IsEnabledButton");
            }
        }


        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { 
                _isReadOnly = value;
                NotifyPropertyChanged("IsReadOnly");
            }
        }

        #endregion

        #region ProprietesVoiture

        private Voiture _selectedVoiture;

        public Voiture SelectedVoiture
        {
            get { return _selectedVoiture; }
            set {
                _selectedVoiture = value;
                NotifyPropertyChanged("SelectedVoiture");
                if (_selectedVoiture != null) IsEnabledButton = true;
            }
        }

        private static ObservableCollection<Voiture> _listeVoiture;
        public static ObservableCollection<Voiture> ListeVoiture {
            get { return _listeVoiture; }
            set
            {
                _listeVoiture = value;

                NotifyPropertyChanged("ListeVoiture");
            }

        }

        
        #endregion

        #region Delegates
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand DelCommand { get; set; }
        public DelegateCommand OpenCommand { get; set; }

        #endregion

        #region Constructeur

        static MainMVVM()
        {
            ListeVoiture = new ObservableCollection<Voiture>();

            ListeVoiture.Add(new Voiture("Mercedes", "A45AMG", "Sport", int.Parse("380"), "Un monstre de puissance.", new BitmapImage(new Uri("U:\\C#\\TP IHM\\MasterDetail\\Images\\a45.jpg"))));
            ListeVoiture.Add(new Voiture("BMW", "M5", "Sport", int.Parse("550"), "Un bolide à tout épreuve.", new BitmapImage(new Uri("U:\\C#\\TP IHM\\MasterDetail\\Images\\m5.jpg"))));
            ListeVoiture.Add(new Voiture("Ford", "Mustang", "Sport", int.Parse("500"), "Attention à vos oreilles.Cette voiture va vous étonner par sa rapidité. Voiture américaine equipé d'un moteur v10.", new BitmapImage(new Uri("U:\\C#\\TP IHM\\MasterDetail\\Images\\mustang.jpg"))));
            ListeVoiture.Add(new Voiture("Mercedes", "C63AMG", "Sport", int.Parse("750"), "Grosse berline sportive allemande.", new BitmapImage(new Uri("U:\\C#\\TP IHM\\MasterDetail\\Images\\c63.jpg"))));
            ListeVoiture.Add(new Voiture("Citroen", "Saxo", "Berline", int.Parse("60"), "Voiture parfaite pour jeune pilote. Ideal pour les plans Tinder", new BitmapImage(new Uri("U:\\C#\\TP IHM\\MasterDetail\\Images\\saxo.jpg"))));
            ListeVoiture.Add(new Voiture("Audi", "RS3", "Sport", int.Parse("367"), "Voiture pour les passionés de la conduite", new BitmapImage(new Uri("U:\\C#\\TP IHM\\MasterDetail\\Images\\rs3.jpg"))));
        }


        public MainMVVM()
            :base()
        {
            TextEdit = "Modifier";
            IsReadOnly = true;
            IsEnabledButton = false;
            IsEnabledAfterEdit = false;
            
            AddCommand = new DelegateCommand(OnAddCommand, CanExecuteAdd);
            EditCommand = new DelegateCommand(OnEditCommand, CanEditCommand);
            DelCommand = new DelegateCommand(OnDelCommand, CanDelCommand);
            OpenCommand = new DelegateCommand(OnOpenCommand, CanOpenCommand);
        }

        #endregion

        #region Methodes

        public void AjouterVoiture()
        {
            
        }

        #endregion

        #region  Evénements

        #region Add
        private void OnAddCommand(object o)
        {
            ButtonPressedEvent.GetEvent().Handler += CloseAddView;

            Add = new AddView();
            Add.Name = "Ajout";

            Add.ShowDialog();

            if(Add.AddViewVM.ClickOnFin)
                ListeVoiture.Add(Add.AddViewVM.NewVoiture);
        }

        private void CloseAddView(object sender, EventArgs e)
        {
            Add.Close();

            ButtonPressedEvent.GetEvent().Handler -= CloseAddView;
        }

        private bool CanExecuteAdd(object o)
        {
            return true;
        }

        #endregion

        #region Edit
        private void OnEditCommand(object o)
        {
            IsReadOnly = !IsReadOnly;
            IsEnabledAfterEdit = !IsEnabledAfterEdit;
            if (TextEdit == "Modifier")
                TextEdit = "Fin Modification";
            else
                TextEdit = "Modifier";

        }
        private bool CanEditCommand(Object o)
        {
            return true;
        }

        #endregion

        #region Del
        private void OnDelCommand(object o)
        {
            ListeVoiture.Remove(SelectedVoiture);
        }

        private bool CanDelCommand(Object o)
        {
            return true;
        }

        #endregion

        #region Open
        private void OnOpenCommand(object obj)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                SelectedVoiture.ImageSource = new BitmapImage(new Uri(op.FileName));
            }
        }

        private bool CanOpenCommand(Object obj)
        {
            return true;
        }

        #endregion

        #endregion
 
    }
}
