using AutoMapper;
using BusinessObject.Model;
using Microsoft.Extensions.DependencyInjection;
using NMSWPF;
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
        private readonly ISystemAccountServices _systemAccountServices;
        private readonly SystemAccount _systemAccount;
        private readonly IMapper _mapper;
        private readonly AdminAccount _adminAccount;
        public StaffWindow(SystemAccount systemAccount)
        {
            InitializeComponent();
            _categoryServices = ((App)Application.Current).ServiceProvider.GetRequiredService<ICategoryServices>();
            _newsArticleServices = ((App)Application.Current).ServiceProvider.GetRequiredService<INewsArticleServices>();
            _systemAccountServices = ((App)Application.Current).ServiceProvider.GetRequiredService<ISystemAccountServices>();
            _systemAccount = systemAccount;
            LoadAccountName(systemAccount.AccountName);
        }

        public void LoadAccountName(string accountName)
        {
            LabelContent.Content = "Welcome Back, " + accountName + ". What do you want to do?";
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
            var manageAccountProfile = new ManageStaffAccountPage(_systemAccountServices,_systemAccount);
            this.MainFrame.Content = manageAccountProfile;
        }

        private void ViewHistory_Click(object sender, RoutedEventArgs e)
        {
            var newsHistory = new NewsHistoryPage(_newsArticleServices, _systemAccount);
            this.MainFrame.Content = newsHistory;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new Login(((App)Application.Current).adminAccount);
            loginWindow.Show();
            this.Close();
        }
    }
}
