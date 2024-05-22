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
    /// Interaction logic for CreateReportPage.xaml
    /// </summary>
    public partial class CreateReportPage : Page
    {
        private readonly INewsArticleServices _newsArticleServices;

        public CreateReportPage(INewsArticleServices newsArticleServices)
        {
            InitializeComponent();
            _newsArticleServices = newsArticleServices;
        }

        private async void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = StartDatePicker.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = EndDatePicker.SelectedDate ?? DateTime.MaxValue;

            var data = await _newsArticleServices.createReport(startDate, endDate);

            ReportDataGrid.ItemsSource = data;
        }
    }
}
