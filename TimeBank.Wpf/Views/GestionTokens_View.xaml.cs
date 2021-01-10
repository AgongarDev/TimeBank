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

namespace TimeBank.Wpf.Views
{
    /// <summary>
    /// Interaction logic for GestionTokens_View.xaml
    /// </summary>
    public partial class GestionTokens_View : UserControl
    {
        private Actions action;
        private AdminManagement AdminMgm;
        public GestionTokens_View()
        {
            InitializeComponent();
            AdminMgm = AdminManagement.GetInstance();
            GetTokensList();
            SwitchState(Actions.LISTADO);
        }

        private void GetTokensList()
        {
            ObservableCollection<Token> tokens = new ObservableCollection<Token>();
            List<Token> tbtokens = AdminMgm.GetTokens();
            foreach (Token item in tbtokens)
            {
                tokens.Add(item);
            }
            db_tokens.ItemsSource = tbtokens;
        }

        private void SwitchState(Actions nAction)
        {
            action = nAction;
            Txt_Title.Text = action.ToString();
            switch (nAction)
            {
                case Actions.IMPORTAR:
                    Frm_listadoUsers.Visibility = Visibility.Collapsed;
                    Frm_listadoUsers_Copy.Visibility = Visibility.Visible;
                    break;
                default:
                    Btn_ValidarUser.Content = action.ToString();
                    Frm_listadoUsers.Visibility = Visibility.Visible;
                    Frm_listadoUsers_Copy.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void Btn_Crear_Click(object sender, RoutedEventArgs e)
        {
            SwitchState(Actions.NUEVO);
        }

        private void Btn_Modificar_Click(object sender, RoutedEventArgs e)
        {
            SwitchState(Actions.MODIFICAR);
        }

        private void Btn_Borrar_Click(object sender, RoutedEventArgs e)
        {
            SwitchState(Actions.BORRAR);
        }

        private void Btn_Exportar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Importar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonValidarUser_Click(object sender, RoutedEventArgs e)
        {
            if (action == Actions.BORRAR)
            {
                BorraToken();
            }

            if (!ValidFrom())
            {
                return;
            }

            Token u = new Token();
            u.Name = Txt_Name.Text;
            u.Hours = int.Parse(Txt_Horas.Text);

            if (AdminMgm.InsertOrUpdate(u))
            {
                MessageBox.Show("Token Registrado!");
            }
            else
            {
                MessageBox.Show("Error en la inserción del Token");
            }
        }

        private void BorraToken()
        {
            Token w = TokenExists();
            if (w == null) return;

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("¿Eliminar el usuario " + w.Name + " : " + w.Hours + "?", "Confirmar Borrado", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (AdminMgm.RemoveToken(w))
                {
                    MessageBox.Show("Usuario Eliminado");
                }
                else
                {
                    MessageBox.Show("Error al intentar eliminar el usuario");
                }
            }
        }

        private Token TokenExists()
        {
            if (CommonLib.ValidateNumEntrance(Txt_Id.Text))
            {
                return AdminMgm.GetTokenById(int.Parse(Txt_Id.Text));
            }
            return null;
        }

        private bool ValidFrom()
        {
            TextBox[] inputs = new TextBox[] { Txt_Name, Txt_Horas };
            foreach (TextBox item in inputs)
            {
                if (string.IsNullOrWhiteSpace(item.Text))
                {
                    MessageBox.Show("  Debe Ingresar un valor en todos los campos. ");
                    return false;
                }
            }
            if (CommonLib.ValidateNumEntrance(Txt_Horas.Text))
            {
                return false;
            }

            return true;
        }

    }
}
