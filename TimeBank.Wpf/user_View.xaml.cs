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
            Txt_Wellcome.Text = "¡Hola " + UserMgm.CurrentUser.Name + "!";
        }

        private void BtnBuscarServicio(object sender, RoutedEventArgs e)
        {
            DataContext = new User_ListaServicios_ViewModel();
        }

        private void BtnOfertarServicio(object sender, RoutedEventArgs e)
        {
            DataContext = new User_ofertarServicio_ViewModel();
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
    }
}
