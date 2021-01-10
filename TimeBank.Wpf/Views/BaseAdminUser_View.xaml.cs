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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimeBank.Wpf.Views
{
    /// <summary>
    /// Interaction logic for BaseAdminUser_View.xaml
    /// </summary>
    public partial class BaseAdminUser_View : UserControl
    {
        public BaseAdminUser_View()
        {
            InitializeComponent();
        }

        private void Gestión_Usuarios_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = new MainWindow();
            win.Show();
            Window parent = Window.GetWindow(this);
            parent.Close();
        }
    }
}
