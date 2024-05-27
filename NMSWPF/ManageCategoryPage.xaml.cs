using BusinessObject.Model;
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
            Loaded += ManageCategoriesPage_Loaded;
        }

        private async void ManageCategoriesPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCategory();
        }

        public async Task LoadCategory()
        {
            var category = await _categoryServices.getAllAsync();
            CategoryDataGrid.ItemsSource = category;
        }
        private async void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectCategory = (Category)CategoryDataGrid.SelectedItem;
                if (selectCategory != null)
                {
                    var result = MessageBox.Show("Are you sure you want to delete this Category?", "Confirm Delete",
                                  MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        var check = await _categoryServices.deleteCategory(selectCategory.CategoryId);
                        if (check == true)
                        {
                            await LoadCategory();
                            MessageBox.Show("Category deleted successfully!");
                        }
                        else
                        {
                            MessageBox.Show("This Category has News Articles!!!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a category to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            var selectCategory = (Category)CategoryDataGrid.SelectedItem;
            if (selectCategory != null)
            {
                var updateCategoryPage = new CreateCategoryPage(_categoryServices, this, selectCategory);
                this.NavigationService.Navigate(updateCategoryPage);
            }
            else
            {
                MessageBox.Show("Please select Category want to update!!");
            }
        }

        private void CreateCategory_Click(object sender, RoutedEventArgs e)
        {
            var createCategory = new CreateCategoryPage(_categoryServices, this);
            this.NavigationService.Navigate(createCategory);
        }

        private void CategoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
