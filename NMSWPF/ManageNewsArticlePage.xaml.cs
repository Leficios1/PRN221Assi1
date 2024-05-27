using BusinessObject.Model;
using Services.DTO.Request;
using Services.DTO.Response;
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
    /// Interaction logic for ManageNewsArticlePage.xaml
    /// </summary>
    public partial class ManageNewsArticlePage : Page
    {
        private readonly INewsArticleServices _newsArticleServices;
        public ManageNewsArticlePage(INewsArticleServices newsArticleServices)
        {
            InitializeComponent();
            _newsArticleServices = newsArticleServices;
            Loaded += ManagenewsArticlePage_Load;
        }
        private async void ManagenewsArticlePage_Load(object sender, RoutedEventArgs e)
        {
            await LoadNewsArticle();
        }

        public async Task LoadNewsArticle()
        {
            try
            {
                var data = await _newsArticleServices.getAll();
                NewsArticlesDataGrid.ItemsSource = data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var search = SearchTextBox.Text;
                var result = await _newsArticleServices.search(search);
                NewsArticlesDataGrid.ItemsSource = result;
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var createNewsArticle = new CreateNewsArticlePage(_newsArticleServices, this);
            await createNewsArticle.LoadCategoryAndTag();
            this.NavigationService.Navigate(createNewsArticle);
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var selectNewArticle = (NewsArticleResponseDTO)NewsArticlesDataGrid.SelectedItem;
            if (selectNewArticle != null)
            {
                var updateNewsArticle = new CreateNewsArticlePage(_newsArticleServices, this, selectNewArticle);
                await updateNewsArticle.LoadCategoryAndTag();
                this.NavigationService.Navigate(updateNewsArticle);
            }
            else
            {
                MessageBox.Show("Please select News Article want to update!!!");
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectDeletedNewsArticle = (NewsArticleResponseDTO)NewsArticlesDataGrid.SelectedItem;
                if (selectDeletedNewsArticle != null)
                {
                    var result = MessageBox.Show("Are you sure you want to delete this Category?", "Confirm Delete",
                                 MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        var flag = await _newsArticleServices.deleteNewsArticle(selectDeletedNewsArticle.NewsArticleId);
                        if(flag == true)
                        {
                            await LoadNewsArticle();
                            MessageBox.Show("Deleted Succesfully");
                        }
                        else
                        {
                            MessageBox.Show("Not Found ID");
                        }
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewsArticlesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
