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
    /// Logique d'interaction pour SignupWindow.xaml
    /// </summary>
    public partial class SignupWindow : Window {

        public SignupMVVM SignupVM;
        public SignupWindow()
        {
            InitializeComponent();
            SignupVM = new SignupMVVM();
            DataContext = SignupVM;
        }
    }
}
