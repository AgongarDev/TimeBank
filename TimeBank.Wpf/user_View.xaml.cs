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
using TimeBank.Bussines.UseCases;
using TimeBank.Wpf.ViewModels;

namespace TimeBank.Wpf
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class user_View : Window
    {
        private readonly UserManagement UserMgm = UserManagement.GetInstance();
        public user_View()
        {
            InitializeComponent();
            CenterWindow();
            Txt_Wellcome.Text = "¡ Hola " + UserMgm.CurrentUser.Name + " !";
            DataContext = new User_Home_ViewModel();
        }

        private void CenterWindow()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void BtnBuscarServicio(object sender, RoutedEventArgs e)
        {
            DataContext = new User_ListaServicios_ViewModel();
        }

        private void BtnGestionarServicio(object sender, RoutedEventArgs e)
        {
            DataContext = new User_GestionServicio_ViewModel();
        }

        private void BtnValidarServicio(object sender, RoutedEventArgs e)
        {
            DataContext = new User_ValidarServicios_ViewModel();
        }

        private void BtnResumenDatos(object sender, RoutedEventArgs e)
        {
            DataContext = new User_ResumenDatos_ViewModel();
        }
        
        private void BtnResumenServicios(object sender, RoutedEventArgs e)
        {
            DataContext = new User_Resumen_ViewModel();
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new User_Home_ViewModel();
        }
    }
}
