using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MasterDetail.ViewModels;

namespace MasterDetail {
    /// <summary>
    /// Logique d'interaction pour InfoView.xaml
    /// </summary>
    public partial class InfoView : Window {

        public InfoMVVM infoVM { get; set; }
        public InfoView(LoginMVVM loginVM)
        {
            InitializeComponent();
            infoVM = new InfoMVVM(loginVM);
            DataContext = infoVM;
        }
    }
}
