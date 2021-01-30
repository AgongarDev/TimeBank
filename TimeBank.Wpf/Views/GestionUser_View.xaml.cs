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
using TimeBank.Bussines;
using TimeBank.Bussines.UseCases;
using TimeBank.Bussines.Utilities;
using TimeBank.Core.Models;
using TimeBank.Wpf.ViewModels;

namespace TimeBank.Wpf.Views
{
    /// <summary>
    /// Interaction logic for GestionUser_View.xaml
    /// </summary>
    public partial class GestionUser_View : UserControl
    {
        private Actions action;
        public GestionUsers_ViewModel vm;

        private readonly UserManagement UserMgm = UserManagement.GetInstance();

        public GestionUser_View()
        {
            InitializeComponent();
            SwitchState(Actions.LISTADO);
        }

        private void ButtonValidarUser_Click(object sender, RoutedEventArgs e)
        {
            if (action == Actions.BORRAR)
            {
                BorrarUser();
                return;
            }

            if (!ValidFrom())
            {
                return;
            }

            User u = new User();
                u.UserId = long.Parse(Txt_UserID.Text);
                u.Name = Txt_UserName.Text;
                u.LastName = Txt_UserLastName.Text;
                u.Address.Street = Txt_Address.Text;
                u.Password = Txt_Pwd.Text;

            if (UserMgm.InsertOrUpdate(u))
            {
                MessageBox.Show("Usuario Registrado!");
            }    
            else
            {
                MessageBox.Show("Error en la inserción del usuario");
            }
        }

        private bool ValidFrom()
        {
            TextBox[] inputs = new TextBox[] { Txt_Address, Txt_UserID, Txt_Pwd, Txt_UserLastName, Txt_UserName };
            foreach (TextBox item in inputs)
            {
                if (string.IsNullOrWhiteSpace(item.Text))
                {
                    MessageBox.Show("  Debe Ingresar un valor en todos los campos. ");
                    return false;
                }
            }
            if (!CommonLib.ValidateNumEntrance(Txt_UserID.Text)) return false;

            return true;
        }

        private void BorrarUser()
        {
            User w = UserExists();
            if (w == null) return;

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("¿Eliminar el usuario " + w.UserId + " : " + w.Name + "?", "Confirmar Borrado", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (UserMgm.RemoveUser(w))
                {
                    MessageBox.Show("Usuario Eliminado");
                } 
                else
                {
                    MessageBox.Show("Error al intentar eliminar el usuario");
                }
            }
        }

        private void Btn_Crear_Click(object sender, RoutedEventArgs e)
        {
            SwitchState(Actions.NUEVO);
        }

        private void Btn_ListarClientes_Click(object sender, RoutedEventArgs e)
        {
            SwitchState(Actions.LISTADO);
        }

        private void Btn_ImportarClientes_Click(object sender, RoutedEventArgs e)
        {
            SwitchState(Actions.IMPORTAR);
        }

        private void Btn_Exportar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exportado a XML");
        }

        private void Btn_Modificar_Click(object sender, RoutedEventArgs e)
        {
            SwitchState(Actions.MODIFICAR);
        }

        private void Btn_Borrar_Click(object sender, RoutedEventArgs e)
        {
            SwitchState(Actions.BORRAR);
        }

        private void SwitchState(Actions nAction)
        {
            action = nAction;
            Txt_Title.Text = action.ToString();
            switch (nAction)
            {
                case Actions.LISTADO:
                    Frm_listadoUsers.Visibility = Visibility.Visible;
                    Frm_listadoUsers_Copy.Visibility = Visibility.Collapsed;
                    GetUserList();
                    break;
                case Actions.IMPORTAR:
                    Frm_listadoUsers.Visibility = Visibility.Collapsed;
                    Frm_listadoUsers_Copy.Visibility = Visibility.Visible;
                    break;
                default:
                    Btn_ValidarUser.Content = action.ToString();
                    Frm_listadoUsers.Visibility = Visibility.Collapsed;
                    Frm_listadoUsers_Copy.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void GetUserList()
        {
            ObservableCollection<User> tbUsers = new ObservableCollection<User>();
            List<User> users = UserMgm.GetUsers();
            foreach (User u in users)
            {
                tbUsers.Add(u);
            }
            db_users.ItemsSource = tbUsers;
        }

        private void Txt_UserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                User u = UserExists();
                if (u != null)
                {
                    if (action == Actions.NUEVO)
                    {
                        MessageBox.Show("Usuario ya creado");
                        SwitchState(Actions.MODIFICAR);
                    }
                    ShowUserData(u);
                }
                
            }
        }

        private User UserExists()
        {
            if (CommonLib.ValidateNumEntrance(Txt_UserID.Text))
            {
                return UserMgm.GetUser(long.Parse(Txt_UserID.Text));
            }
            return null;
        }

        private void ShowUserData(User u)
        {
            Txt_UserName.Text = u.Name;
            Txt_UserLastName.Text = u.LastName;
            Txt_Address.Text = u.Address.Street;
            Txt_Pwd.Text = u.Password;
            CBox_IsAdmin.IsChecked = u.Admin;
        }
    }
}