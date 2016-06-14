using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Metier;
using MasterDetail;

namespace ClassLibrary
{
    public class Class
    {
        public void Save(ObservableCollection<Voiture> listeVoiture)
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

        public void Load()
        {

        }
    }
}
