using Microsoft.Extensions.DependencyInjection;
using NMSWPF;
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
using System.Windows.Shapes;

namespace TrinhLekhoaWPF
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private readonly ISystemAccountServices _systemaccountServices;
        public AdminWindow()
        {
            InitializeComponent();
            _systemaccountServices = ((App)Application.Current).ServiceProvider.GetRequiredService<ISystemAccountServices>();
        }

        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
        private void ManageAccountButton_Click(object sender, RoutedEventArgs e)
        {
            var manageAccountPage = new ManageAccountPage(_systemaccountServices);
            this.Content = manageAccountPage;
        }

        private void CreateReportButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CreateReportPage());
        }
    }
}
