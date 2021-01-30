using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimeBank.Bussines;
using TimeBank.Bussines.UseCases;
using TimeBank.Wpf.ViewModels;

namespace TimeBank.Wpf
{
    /// <summary>
    /// Lógica de interacción para Window2.xaml
    /// </summary>
    public partial class admin_View : Window
    {

        public admin_View()
        {
            InitializeComponent();
            CenterWindow();
            DataContext = new AdminUser_Base_ViewModel();
        }

        private void CenterWindow()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void Btn_GestionTokens_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new GestionTokens_ViewModel();
        }

        private void Btn_HomeApp_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AdminUser_Base_ViewModel();
        }

        private void Btn_GestiónUsuarios_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new GestionUsers_ViewModel();
        }

        private void Btn_GestionServicios_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new GestionServicios_ViewModel();
        }
    }
}
