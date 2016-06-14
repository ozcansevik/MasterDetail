﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MasterDetail.ViewModels;
using Metier;

namespace MasterDetail
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// 
   
        
    public partial class MainWindow : Window
    {
        public MainMVVM VoitureViewModel { get; set; }

        public MainWindow(LoginMVVM loginVM)
        {
            InitializeComponent();

            VoitureViewModel = new MainMVVM(loginVM);
            DataContext = VoitureViewModel;

        }
    }
}
