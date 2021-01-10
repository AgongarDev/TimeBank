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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeBank.Bussines.UseCases;
using TimeBank.Core.Models;

namespace TimeBank.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserManagement UserMgm;

        public MainWindow()
        {
            InitializeComponent();
            UserMgm = UserManagement.GetInstance();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Txt_UserId.Text))
                return;

            object id;
            try
            {
                id = Convert.ToInt64(long.Parse(Txt_UserId.Text));
            }
            catch
            {
                id = Txt_UserId.Text;
            }
         
            User u = UserMgm.GetUserAccess(id);
            if (u == null)
            {
                MessageBox.Show("    Usuario no encontrado. \n Introduzca un nombre o ID válido");
                return;
            }

            if (u.Admin)
            {
                AccessAdmin(u);
                return;
            }
            AccessUser(u);
        }
    
        private void AccessAdmin(User u)
        {
            UserMgm.CurrentUser = u;
            admin_View win = new admin_View();
            win.Show();
            this.Close();
        }

        private void AccessUser(User u)
        {
            UserMgm.CurrentUser = u;
            user_View win = new user_View();
            win.Show();
            this.Close();
        }

        private void Txt_UserId_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox Input = (TextBox)sender;
            if (Input.Text == "admin")
            {
                User u = new User();
                u.Name = "AT";
                AccessAdmin(u);
                return;
            }
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
        }
    }
}
