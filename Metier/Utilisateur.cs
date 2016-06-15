using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Metier {
    public class Utilisateur : VMBase {

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; NotifyPropertyChanged("UserName"); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; NotifyPropertyChanged("Password"); }
        }

        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; NotifyPropertyChanged("Type"); }
        }

        private string _nom;

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; NotifyPropertyChanged("Nom"); }
        }

        private string _prenom;

        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; NotifyPropertyChanged("Prenom"); }
        }

        private string _age;

        public string Age
        {
            get { return _age; }
            set { _age = value; NotifyPropertyChanged("Age"); }
        }

        private string _adresse;

        public string Adresse
        {
            get { return _adresse; }
            set { _adresse = value; NotifyPropertyChanged("Adresse"); }
        }

        

        private string m_imagePath;
        public string ImagePath
        {
            get { return m_imagePath; }
            set
            {
                m_imagePath = value;
                NotifyPropertyChanged("ImagePath");


            }
        }

        public Utilisateur(string username, string password, string type, string nom, string prenom, string age, string imagepath, string adresse)
        {
            UserName = username;
            Password = password;
            Type = type;
            Nom = nom;
            Prenom = prenom;
            Age = age;
            ImagePath = imagepath;
            Adresse = adresse;
        }

        public Utilisateur() { }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;

            Utilisateur u = obj as Utilisateur;
            if (u == null) return false;

            return UserName.Trim().Equals(u.UserName) && Password.Trim().Equals(u.Password);
        }
    }

}

