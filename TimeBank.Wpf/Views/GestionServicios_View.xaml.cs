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
    /// Interaction logic for GestionServicios_View.xaml
    /// </summary>
    public partial class GestionServicios_View : UserControl
    {
        AdminManagement admin = AdminManagement.GetInstance();

        public GestionServicios_View()
        {
            InitializeComponent();
            List<Category> cats = null;

            cats = admin.GetCategories();
            ObservableCollection<Category> tbcats = new ObservableCollection<Category>();
            foreach (Category cate in cats)
            {
                tbcats.Add(cate);
            }
            Db_Categoria.ItemsSource = tbcats;
        }

        private void Btn_ListarClientes_Click(object sender, RoutedEventArgs e)
        {
            FrmCat.Visibility = Visibility.Collapsed;
        }

        private void Btn_Crear_Click(object sender, RoutedEventArgs e)
        {
            FrmCat.Visibility = Visibility.Visible;
        }

        private void Btn_nuevaCat_Click(object sender, RoutedEventArgs e)
        {
            ValidateEntranceCat();
            var existst = admin.GetCategory(Txt_NombreCat.Text);
            if (existst != null)
            {
                MessageBox.Show("Categoría duplicada");
                return;
            }
            admin.InsertOrUpdate(new Category
            {
                Name = Txt_NombreCat.Text
            });
        }

        private void Btn_modifyCat_Click(object sender, RoutedEventArgs e)
        {
            var existst = admin.GetCategory(Txt_NombreCat.Text);
            if (existst == null)
            {
                MessageBox.Show("La categoría no existe");
                return;
            }
            admin.InsertOrUpdate(new Category
            {
                Name = Txt_NombreCat.Text
            });
        }    
        
        private void Btn_Eliminar_Click(object sender, RoutedEventArgs e)
        {
            var existst = admin.GetCategory(Txt_NombreCat.Text);
            if (existst == null)
            {
                return;
            }
            return; // Todo
        }

        private bool ValidateEntranceCat()
        {
            if (string.IsNullOrWhiteSpace(Txt_NombreCat.Text))
            {
                return false;
            }
            return true;

        }
    }
}
