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
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var createNewsArticle = new CreateNewsArticlePage(_newsArticleServices, this);
            this.NavigationService.Navigate(createNewsArticle);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var selectNewArticle = (NewsArticleResponseDTO)NewsArticlesDataGrid.SelectedItem;
            if (selectNewArticle != null)
            {
                var updateNewsArticle = new CreateNewsArticlePage(_newsArticleServices,this);
                this.NavigationService.Navigate(updateNewsArticle);
            }
            else
            {
                MessageBox.Show("Please select News Article want to update!!!");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewsArticlesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
