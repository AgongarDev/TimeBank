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
using TimeBank.Bussines.Utilities;
using TimeBank.Core.Models;
using System.Linq;
using TimeBank.Core.Enums;

namespace TimeBank.Wpf.Views
{
    /// <summary>
    /// Interaction logic for User_ListaServicios.xaml
    /// </summary>
    public partial class User_ListaServicios : UserControl
    {
        ObservableCollection<Service> tbservicios = new ObservableCollection<Service>();
        List<Service> servicios = new List<Service>();
        AdminManagement AdminMgm = AdminManagement.GetInstance();
        Service ser;

        public User_ListaServicios()
        {
            InitializeComponent();
            ShowResults();
            GetServicios();
        }

        private void ShowResults()
        {
            ser = null;
            DetalleServicio.Visibility = Visibility.Collapsed;
        }

        private void GetServicios()
        {
            var categorias = AdminMgm.GetCategories();
            cb_Categorias.ItemsSource = categorias;

            servicios = AdminMgm.GetAllServices();
            
            if (servicios.Count < 0) return; 

            foreach (Service item in servicios)
            {
                tbservicios.Add(item);
            }
            Dg_Servicios.ItemsSource = tbservicios;
        }

        private void BtnBuscarServicio_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Txt_ServicioID.Text))
            {
                if (CommonLib.ValidateNumEntrance(Txt_ServicioID.Text))
                {
                    ListCollectionView coll = new ListCollectionView(tbservicios);
                    coll.Filter = (e) =>
                    {
                        ser = e as Service;
                        if (ser.ServiceID == long.Parse(Txt_ServicioID.Text))
                        {
                            return true;
                        }
                        return false;
                    };
                    Dg_Servicios.ItemsSource = coll;
                }
                return;
            }
            
            if (!string.IsNullOrWhiteSpace(Txt_NombreServicio.Text))
            {
                ListCollectionView coll = new ListCollectionView(tbservicios);
                coll.Filter = (e) =>
                {
                    ser = e as Service;
                    if (ser.Name == Txt_ServicioID.Text)
                    {
                        return true;
                    }
                    return false;
                };
                Dg_Servicios.ItemsSource = coll;
                return;
            }

            if (cb_Categorias.SelectedItem != null)
            {
                ListCollectionView coll = new ListCollectionView(tbservicios);
                coll.Filter = (e) =>
                {
                    ser = e as Service;
                    Category category = cb_Categorias.SelectedItem as Category;
                    if (ser.CategoryID == category.ID)
                    {
                        return true;
                    }
                    return false;
                };
                Dg_Servicios.ItemsSource = coll;
                return;
            }
            Dg_Servicios.ItemsSource = tbservicios;
        }

        private void Dg_Servicios_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ser = sender as Service;
            if (ser == null)
            {
                return;
            }
            DetalleServicio.Visibility = Visibility.Visible;
            DetalleServicio.DataContext = ser;
            int precio = 0;
            foreach (Token t in ser.Price)
            {
                precio += t.Hours;
            }
            if (ser.ProviderID == UserManagement.GetInstance().CurrentUser.UserId)
            {
                Txt_ServicioPropio.Visibility = Visibility.Visible;
                Btn_Demandar.IsEnabled = false;
            } else
            {
                Txt_ServicioPropio.Visibility = Visibility.Hidden;
                Btn_Demandar.IsEnabled = true;
            }
            Txt_PrecioServicio.Text = precio.ToString();
        }

        private void Btn_Demandar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("¿Adquirir servicio? ", "Confirmar Adquisición", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                User u = UserManagement.GetInstance().CurrentUser;
                try
                {
                    AdminMgm.NewPayment(u, ser, PaymentType.Direct);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Compruebe su saldo en tokens o hable con un administrador de TimeBank\n \n Detalle: " + ex.Message, "Alerta: SERVICIO NO ADQUIRIDO", MessageBoxButton.OK, MessageBoxImage.Information); ;
                }
            }
        }

        private void Btn_ServiceBack_Click(object sender, RoutedEventArgs e)
        {
            ShowResults();
        }
    }
}
