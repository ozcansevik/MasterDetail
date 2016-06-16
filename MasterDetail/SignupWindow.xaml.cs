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
using Microsoft.Maps.MapControl.WPF.Design;
using Microsoft.Maps.MapControl.WPF;

namespace MasterDetail {
    /// <summary>
    /// Logique d'interaction pour SignupWindow.xaml
    /// </summary>
    public partial class SignupWindow : Window {

        public SignupMVVM SignupVM;
  
        LocationConverter locConverter = new LocationConverter();
        public LoginMVVM Login { get; set; }
        public Location PositionPin { get; set; }
        public SignupWindow(LoginMVVM login)
        {
            InitializeComponent();
            Login = login;
            SignupVM = new SignupMVVM(this);
            DataContext = SignupVM;
            myMap.Focus();
        }
        private void MapWithPushpins_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Disables the default mouse double-click action.
            e.Handled = true;

            // Determin the location to place the pushpin at on the map.

            //Get the mouse click coordinates
            Point mousePosition = e.GetPosition(this);
            mousePosition.X-= 283;
            mousePosition.Y -= 30;

            //Convert the mouse coordinates to a locatoin on the map
            Location pinLocation = myMap.ViewportPointToLocation(mousePosition);

            // The pushpin to add to the map.
            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;

            // Adds the pushpin to the map.
            myMap.Children.Add(pin);
            PositionPin = pinLocation;
        }

    }

}

