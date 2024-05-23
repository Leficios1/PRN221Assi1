using BusinessObject.Model;
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
    /// Interaction logic for NewsHistoryPage.xaml
    /// </summary>
    public partial class NewsHistoryPage : Page
    {
        private readonly INewsArticleServices _newsArticleServices;
        private readonly SystemAccount _systemAcount;
        public NewsHistoryPage(INewsArticleServices newsArticleServices, SystemAccount systemAccount)
        {
            InitializeComponent();
            _newsArticleServices = newsArticleServices;
            _systemAcount = systemAccount;
            LoadHistory();
        }
        private async void LoadHistory()
        {
            try
            {
                var data = await _newsArticleServices.getByAccountId(_systemAcount.AccountId);
                NewsArticlesDataGrid.ItemsSource = data;
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewsArticlesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
