using DataAccessObject.Repository.Interface;
using Services.Services.Interface;
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

namespace TrinhLekhoaWPF
{
    /// <summary>
    /// Interaction logic for ManageCategory.xaml
    /// </summary>
    public partial class ManageCategoryPage : Page
    {
        private readonly ICategoryServices _categoryServices;
        public ManageCategoryPage(ICategoryServices categoryServices)
        {
            InitializeComponent();
            _categoryServices = categoryServices;
            LoadCategory();
        }

        private async void LoadCategory()
        {
            var category = await _categoryServices.getAllAsync();
            CategoryDataGrid.ItemsSource = category;
        }
        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            var staffWindow = Application.Current.Windows.OfType<StaffWindow>().FirstOrDefault();

            if (staffWindow != null)
            {
                staffWindow.Show();
                Application.Current.MainWindow.Close();
            }
        }

        private void UpdateCategory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateCategory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CategoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
