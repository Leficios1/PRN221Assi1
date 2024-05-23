using AutoMapper;
using BusinessObject.Model;
using Microsoft.Extensions.DependencyInjection;
using NMSWPF;
using Services.DTO.Request;
using Services.Services;
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
    /// Interaction logic for CreateCategoryPage.xaml
    /// </summary>
    public partial class CreateCategoryPage : Page
    {
        private readonly ICategoryServices _categoryServices;
        private readonly ManageCategoryPage _manageCategoryPage;
        private readonly Category _categoryToUpdate;
        private readonly IMapper _mapper;
        public CreateCategoryPage(ICategoryServices categoryService, ManageCategoryPage manageCategoryPage, Category categoryToUpdate = null)
        {
            InitializeComponent();
            _categoryServices = categoryService;
            _manageCategoryPage = manageCategoryPage;
            _categoryToUpdate = categoryToUpdate;
            _mapper = ((App)Application.Current).ServiceProvider.GetRequiredService<IMapper>();

            if (_categoryToUpdate != null)
            {
                CategoryNameTextBox.Text = _categoryToUpdate.CategoryName;
                CategoryDescriptionTextBox.Text = _categoryToUpdate.CategoryDesciption;
                CreateButton.Content = "Update";
            }
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var categoryName = CategoryNameTextBox.Text;
                var categoryDescription = CategoryDescriptionTextBox.Text;
                if (_categoryToUpdate == null)
                {
                    var newCategory = new CategoryRequestDTO
                    {
                        CategoryName = categoryName,
                        CategoryDesciption = categoryDescription
                    };

                    var flag = await _categoryServices.createCategory(newCategory);
                    if (flag == null)
                    {
                        MessageBox.Show("Id has Exist");
                    }
                    MessageBox.Show("Category created successfully!");
                    await _manageCategoryPage.LoadCategory();
                    NavigationService.GoBack();
                }
                else
                {
                    _categoryToUpdate.CategoryName = categoryName;
                    _categoryToUpdate.CategoryDesciption = categoryDescription;

                    var mapper = _mapper.Map<CategoryUpdateRequestDTO>(_categoryToUpdate);
                    var update = await _categoryServices.updateCategory(mapper);
                    if(update == null)
                    {
                        MessageBox.Show("Error while update Category!");
                    }
                    MessageBox.Show("Update suceesful!");
                }
                    await _manageCategoryPage.LoadCategory();
                    NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
