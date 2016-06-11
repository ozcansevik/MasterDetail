using System;
using System.Collections.Generic;

using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class VMBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static List<string> ListeTypeVoiture { get; set; }

        static VMBase()
        {

            ListeTypeVoiture = new List<string>();
            ListeTypeVoiture.Add("Familiale");
            ListeTypeVoiture.Add("Sport");
            ListeTypeVoiture.Add("Break");
            ListeTypeVoiture.Add("Berline");
            ListeTypeVoiture.Add("Coupé");
            ListeTypeVoiture.Add("4x4");
 
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

