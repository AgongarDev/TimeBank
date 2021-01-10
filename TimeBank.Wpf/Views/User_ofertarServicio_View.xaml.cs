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
using TimeBank.Bussines.UseCases;
using TimeBank.Bussines.Utilities;
using TimeBank.Core.Models;

namespace TimeBank.Wpf.Views
{
    /// <summary>
    /// Interaction logic for User_ListaServicios.xaml
    /// </summary>
    public partial class User_ofertarServicio_View : UserControl
    {
        AdminManagement AdminMgm = AdminManagement.GetInstance();

        public User_ofertarServicio_View()
        {
            InitializeComponent();
            DatipiIni.DisplayDateStart = System.DateTime.Now;
        }

        private void Btn_Ofertar_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ValidForm();
            }
            catch (Exception ex) 
            { 
                MessageBox.Show("No se ha guardado el servicio. " + ex.Message);
                return;
            }

            List<Token> precio = SetPrice(int.Parse(Txt_Precio.Text));
            try
            {
                AdminMgm.InsertOrUpdate(new Service
                {
                    Name = Txt_NombreServicio.Text,
                    Description = Txt_DescServicio.Text,
                    Price = precio,
                    Available = true,
                    Category = (Category)Cb_Categorias.SelectedItem,
                    Provider = UserManagement.GetInstance().CurrentUser,
                    DateIni = (DateTime)DatipiIni.SelectedDate,
                    DateEnd = (DateTime)DatipiEnd.SelectedDate,
                });
                MessageBox.Show("Servicio Guardado con Éxito");
                LimpiaForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha guardado el servicio. Inténtalo más tarde" +
                    "\n \n "+ ex.Message);
            }
        }

        private List<Token> SetPrice(int v)
        {
            List<Token> t = new List<Token>();
            List<Token> tokens = AdminMgm.GetTokens();
            while (v > 0)
            {
                foreach (Token tk in tokens)
                {
                    do
                    {
                        t.Add(tk);
                    }
                    while (v <= tk.Hours);
                }
            }
            return t;
        }

        private void ValidForm()
        {
            TextBox[] formItems = new TextBox[] { Txt_DescServicio, Txt_NombreServicio, Txt_Precio };
            foreach (TextBox i in formItems)
            {
                TextBox nItem = (TextBox)i;
                if (string.IsNullOrWhiteSpace(nItem.Text))
                {
                    throw new ArgumentException("Completa todos los campos");
                }
            }

            if (Cb_Categorias.SelectedItem == null)
            {
                throw new ArgumentException("Selecciona una categoría");
            }

            if (!CommonLib.ValidateNumEntrance(Txt_Precio.Text))
            {
                throw new ArgumentException("Introduce un precio en horas, válido. (Sólo números)");
            }

            if ((DatipiIni.SelectedDate == null) || (DatipiEnd.SelectedDate == null))
            {
                throw new ArgumentException("Selecciona una fecha de inicio y una fecha de fin de servicio");
            }

            if (DatipiIni.SelectedDate > DatipiEnd.SelectedDate)
            {
                throw new ArgumentException("La fecha inicial debe ser menor a la fecha final");
            }
        }

        private void InitView()
        {
            var categorias = AdminMgm.GetCategories();
            Cb_Categorias.ItemsSource = categorias;
        }

        private void Btn_limpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiaForm();
        }

        private void LimpiaForm()
        {
            Txt_NombreServicio.Text = string.Empty;
            Txt_DescServicio.Text = string.Empty;
            Txt_Precio.Text = string.Empty;
        }
    }
}
