using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeBank.Bussines.UseCases;
using TimeBank.Core.Models;
using Validation = TimeBank.Core.Models.Validation;

namespace TimeBank.Wpf.Views
{
    /// <summary>
    /// Interaction logic for User_ListaServicios.xaml
    /// </summary>
    public partial class User_ValidarServicios : UserControl
    {
        public User_ValidarServicios()
        {
            InitializeComponent();
            grid_lista.DataContext = UserManagement.GetInstance().CurrentUser;
            ShowList();
        }

        private void ShowList()
        {
            grid_validar.Visibility = Visibility.Hidden;
            List<Service> servicios = new List<Service>();
            foreach (Validation v in UserManagement.GetInstance().CurrentUser.Validations)
            {
               servicios.Add(AdminManagement.GetInstance().GetService(v.ServiceID));
            }
            ObservableCollection<Service> tbservicios = new ObservableCollection<Service>();
            Dg_userServices.ItemsSource = tbservicios;
        }

        private void Btn_Demandar_Copy_Click(object sender, RoutedEventArgs e)
        {
            ShowList();
        }

        private void Dg_userServices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            grid_validar.Visibility = Visibility.Visible;
        }
    }
}
