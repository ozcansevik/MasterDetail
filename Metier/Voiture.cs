using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Metier
{
    public class Voiture : VMBase
    {

        private string _marque;

        public string Marque
        {
            get { 
                return _marque; 
            }
            set {
                _marque = value;
                NotifyPropertyChanged("Marque");
            }
        }

        private string _modele;

        public string Modele
        {
            get
            {
                return _modele;
            }
            set {
                _modele = value;
                NotifyPropertyChanged("Modele");
            
            }
        }

        private string _type;

        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");

            }
        }

        private int _puissance;

        public int Puissance
        {
            get
            {
                return _puissance;
            }
            set
            {
                _puissance = value;
                NotifyPropertyChanged("Puissance");

            }
        }

        private string _description;

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");

            }
        }

        private BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get
            {
                return _imageSource;
            }

            set
            {
                _imageSource = value;
                NotifyPropertyChanged("ImageSource");
            }
        }


        public Voiture(string marque, string modele, string type, int puissance, string description, BitmapImage image)
        {
            Marque = marque;
            Modele = modele;
            Puissance = puissance;
            Description = description;
            Type = type;
            ImageSource = image;
        }

        public override string ToString()
        {
            return Marque +" "+ Modele;
        }

    }

    /*
    public class VoitureFamiliale : Voiture
    {
        public int NbPlaces { get; set; }
        public int TailleCoffre { get; set; }
        
        public VoitureFamiliale(string marque, string modele, string type, int puissance, string description, int nbplaces, int taillecoffre)
            :base(marque,modele,type,puissance,description)
        {
            NbPlaces = nbplaces;
            TailleCoffre = taillecoffre;
        }

    }
    */
}
