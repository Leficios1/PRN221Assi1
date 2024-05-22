using Microsoft.Extensions.DependencyInjection;
using NMSWPF;
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
using System.Windows.Shapes;

namespace TrinhLekhoaWPF
{
    /// <summary>
    /// Interaction logic for StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : Window
    {
        private readonly ICategoryServices _categoryServices;
        private readonly INewsArticleServices _newsArticleServices;
        public StaffWindow()
        {
            InitializeComponent();
            _categoryServices = ((App)Application.Current).ServiceProvider.GetRequiredService<ICategoryServices>();
            _newsArticleServices = ((App)Application.Current).ServiceProvider.GetRequiredService<INewsArticleServices>();
        }

        private void ManageCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            var manageCategory = new ManageCategoryPage(_categoryServices);
            this.MainFrame.Content = manageCategory;
        }

        private void ManageNewsArticleButton_Click(object sender, RoutedEventArgs e)
        {
            var manageNewsArticle = new ManageNewsArticlePage(_newsArticleServices);
            this.MainFrame.Content = manageNewsArticle;
        }

        private void ManageProfileButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewHistory_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
