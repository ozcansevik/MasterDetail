using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metier;
using Library;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using MasterDetail.Events;

namespace MasterDetail.ViewModels
{
    public class AddMVVM : VMBase
    {
        public bool ClickOnFin { get; set; }

        #region ProprietesVoiture

        private Voiture _newVoiture;

        public Voiture NewVoiture
        {
            get
            {
                return _newVoiture;
            }
            set
            {
                _newVoiture = value;
                NotifyPropertyChanged("NewVoiture");
            }
        }

        #endregion

        #region Delegates

        public DelegateCommand FinCommand { get; set; }
        public DelegateCommand OpenCommand { get; set; }

        public DelegateCommand SoundCommand { get; set; }

        #endregion

        #region Constructeur

        public AddMVVM()
            : base()
        {
            ClickOnFin = false;
            FinCommand = new DelegateCommand(OnFinCommand, CanExecuteFin);
            OpenCommand = new DelegateCommand(OnOpenCommand, CanOpenCommand);
            SoundCommand = new DelegateCommand(OnSoundCommand, CanSoundCommand);

            NewVoiture = new Voiture("", "", "Familiale", 0, "", "Images/image.png","");

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
               NewVoiture.ImagePath = op.FileName;
            }
        }

        private bool CanOpenCommand(Object obj)
        {
            return true;
        }

        #endregion

        #region Fin

        private void OnFinCommand(object obj)
        {
            ClickOnFin = true;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }
        private bool CanExecuteFin(object obj)
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
                NewVoiture.SoundPath=openFileDialog.FileName;

        }
        private bool CanSoundCommand(object obj)
        {
            return true;
        }

        #endregion

        #endregion
    }
}
