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
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MasterDetail.ViewModels
{

    [Serializable]

    public class MainMVVM : VMBase{

        private MediaState _audioControl;

        public MediaState AudioControl
        {
            get
            {
                return _audioControl;
            }

            set
            {
                _audioControl = value;
                NotifyPropertyChanged("AudioControl");
            }
        }



        private Visibility _isVendeur;

        public Visibility IsVendeur
        {
            get
            {
                return _isVendeur;
            }

            set
            {
                _isVendeur = value;
                NotifyPropertyChanged("IsVendeur");
            }
        }

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
                if (_selectedVoiture != null)
                {
                    AudioControl = MediaState.Play;
                    IsEnabledButton = true;
                }
            }
        }

        private ObservableCollection<Voiture> _listeVoiture;

        public ObservableCollection<Voiture> ListeVoiture {
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

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand LoadCommand { get; set; }
        public DelegateCommand InfoCommand { get; set; }
        public DelegateCommand QuitCommand { get; set; }
        public DelegateCommand SoundCommand { get; set; }

        #endregion

        #region Views
        public MainWindow MainWindow { get; set; }
        public AddView Add { get; set; }
        public InfoView InfoVue { get; set; }

        #endregion

        #region Constructeur

        public MainMVVM(MainWindow mainWindow)
            :base()
        {
            MainWindow = mainWindow;

            ListeVoiture = new ObservableCollection<Voiture>();

            if (MainWindow.LoginVM.Utilisateur.Type.Equals("Vendeur"))
                IsVendeur = Visibility.Visible;
            else
                IsVendeur = Visibility.Hidden;

            ListeVoiture.Add(new Voiture("Mercedes", "A45AMG", "Sport", int.Parse("380"), "Un monstre de puissance.", "Images/a45.jpg","Sounds/a45.wma"));
            ListeVoiture.Add(new Voiture("BMW", "M5", "Sport", int.Parse("550"), "Un bolide à tout épreuve.", "Images/m5.jpg", "Sounds/m5.wma"));
            ListeVoiture.Add(new Voiture("Ford", "Mustang", "Sport", int.Parse("500"), "Attention à vos oreilles.Cette voiture va vous étonner par sa rapidité. Voiture américaine equipé d'un moteur v10.","Images/mustang.jpg","Sounds/mustang.wma"));
            ListeVoiture.Add(new Voiture("Mercedes", "C63AMG", "Sport", int.Parse("750"), "Grosse berline sportive allemande.", "Images/c63.jpg","Sounds/c63.wma"));
            ListeVoiture.Add(new Voiture("Citroen", "Saxo", "Berline", int.Parse("60"), "Voiture parfaite pour jeune pilote. Ideal pour les plans Tinder", "Images/saxo.jpg", "Sounds/saxo.wma"));
            ListeVoiture.Add(new Voiture("Audi", "RS3", "Sport", int.Parse("367"), "Voiture pour les passionés de la conduite", "Images/rs3.jpg","Sounds/rs3.wma"));

            TextEdit = "Modifier";
            IsReadOnly = true;
            IsEnabledButton = false;
            IsEnabledAfterEdit = false;
            AudioControl = MediaState.Manual;

            AddCommand = new DelegateCommand(OnAddCommand, CanExecuteAdd);
            EditCommand = new DelegateCommand(OnEditCommand, CanEditCommand);
            DelCommand = new DelegateCommand(OnDelCommand, CanDelCommand);
            OpenCommand = new DelegateCommand(OnOpenCommand, CanOpenCommand);
            SaveCommand = new DelegateCommand(OnSaveCommand, CanSaveCommand);
            LoadCommand = new DelegateCommand(OnLoadCommand, CanLoadCommand);
            InfoCommand = new DelegateCommand(OnInfoCommand, CanInfoCommand);
            QuitCommand = new DelegateCommand(OnQuitCommand, CanQuitCommand);
            SoundCommand = new DelegateCommand(OnSoundCommand, CanSoundCommand);
        }

        #endregion

        #region  Evénements

        #region Info
        private void OnInfoCommand(object obj)
        {
            AudioControl = MediaState.Stop;
            ButtonPressedEvent.GetEvent().Handler += CloseInfoView;

            InfoVue = new InfoView(MainWindow.LoginVM);

           
            InfoVue.ShowDialog();

            if (InfoVue.infoVM.ClickOnFin)
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Utilisateur>));

                StreamWriter wr = new StreamWriter("Xml/listeUtilisateur.xml");

                xs.Serialize(wr, MainWindow.LoginVM.ListUtilisateur);

                if (InfoVue.infoVM.ClickOnDel)
                {
                    MainWindow.Hide();
                    MainWindow.LoginVM.Login.Show();
                }
            }
        }

        private bool CanInfoCommand(object obj)
        {
            return true;
        }

        private void CloseInfoView(object sender, EventArgs e)
        {
            InfoVue.Close();

            ButtonPressedEvent.GetEvent().Handler -= CloseInfoView;
        }

        #endregion

        #region Quit

        private void OnQuitCommand(object obj)
        {
            MainWindow.LoginVM.Login.Show();
            MainWindow.Hide();
            AudioControl = MediaState.Stop;
        }
        private bool CanQuitCommand(object obj)
        {
            return true;
        }

        #endregion

        #region Add
        private void OnAddCommand(object o)
        {
            AudioControl = MediaState.Stop;
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
            op.Title = "Selectionner une image";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                SelectedVoiture.ImagePath = op.FileName;
            }
        }

        private bool CanOpenCommand(Object obj)
        {
            return true;
        }

        #endregion

        #region Save
        private void OnSaveCommand(object o)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Sauvegarder un fichier .xml";
            saveDialog.FileName = ".xml";
            saveDialog.Filter = "XML-File | *.xml";
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Voiture>));

            if (saveDialog.ShowDialog() == true)
            {
                using (StreamWriter wr = new StreamWriter(saveDialog.FileName))
                {
                    xs.Serialize(wr, ListeVoiture);
                }
            }
        }

        private bool CanSaveCommand(Object o)
        {
            return true;
        }

        #endregion

        #region Load
        private void OnLoadCommand(object o)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Selectionner un fichier .xml";
            op.Filter = "XML-File | *.xml";
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Voiture>));

            if (op.ShowDialog() == true)
            {
                using (StreamReader rd = new StreamReader(op.FileName))
                {
                    ObservableCollection<Voiture> listeVoitureChargee = xs.Deserialize(rd) as ObservableCollection<Voiture>;
                    this.ListeVoiture = listeVoitureChargee;
                }
            }

        }

        private bool CanLoadCommand(Object o)
        {
            return true;
        }

        #endregion

        #region Sound

        private void OnSoundCommand(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "WMA files (*.wma,*.mp3)|*.wma;*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                SelectedVoiture.SoundPath = openFileDialog.FileName;

        }
        private bool CanSoundCommand(object obj)
        {
            return true;
        }

        #endregion

        #endregion

    }
}
