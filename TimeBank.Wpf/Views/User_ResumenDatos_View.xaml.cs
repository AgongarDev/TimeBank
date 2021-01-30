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

namespace TimeBank.Wpf.Views
{
    /// <summary>
    /// Interaction logic for User_ResumenDatos_View.xaml
    /// </summary>
    public partial class User_ResumenDatos_View : UserControl
    {
        public User_ResumenDatos_View()
        {
            InitializeComponent();
            grid_userDatos.DataContext = UserManagement.GetInstance().CurrentUser;
        }
        private void Btn_guardar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
