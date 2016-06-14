using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Metier
{
    [Serializable]

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

        [XmlIgnore]
        private BitmapImage _image;
        [XmlIgnore]
        public BitmapImage Image
        {
            get
            {
                return _image;
            }

            set
            {
                _image = value;
                NotifyPropertyChanged("Image");
            }
        }

        private string m_imagePath;
        public string ImagePath
        {
            get { return m_imagePath; }
            set
            {
                m_imagePath = value;
                Image = new BitmapImage(new Uri(m_imagePath));
            }
        }

        public Voiture(string marque, string modele, string type, int puissance, string description, string imagepath)
        {
            Marque = marque;
            Modele = modele;
            Puissance = puissance;
            Description = description;
            Type = type;
            ImagePath = imagepath;

        }

        public Voiture()
        {
         
        }
        public override string ToString()
        {
            return Marque +" "+ Modele;
        }

    }

    
}
