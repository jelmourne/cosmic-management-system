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

namespace cosmic_management_system.View.UserPage {
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page {
        private MainWindow mainWindow;
        public HomePage(MainWindow mainWindow) {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }
       

        private void Button_Click(object sender, RoutedEventArgs e) {
            mainWindow.updateFrame(new LoginPage(mainWindow));
        }
    }
}
