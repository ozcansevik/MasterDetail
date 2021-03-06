﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Library
{
    public class VMBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static List<string> ListeTypeVoiture { get; set; }
        public static List<string> ListTypeUtilisateur { get; set; }

        static VMBase()
        {

            ListeTypeVoiture = new List<string>();
            ListeTypeVoiture.Add("Familiale");
            ListeTypeVoiture.Add("Sport");
            ListeTypeVoiture.Add("Break");
            ListeTypeVoiture.Add("Berline");
            ListeTypeVoiture.Add("Coupé");
            ListeTypeVoiture.Add("4x4");

            ListTypeUtilisateur = new List<string>();
            ListTypeUtilisateur.Add("Vendeur");
            ListTypeUtilisateur.Add("Client");

        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

      
    }
}

