using Services.DTO.Response;
using Services.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ManageAccountPage.xaml
    /// </summary>
    /// 
    public partial class ManageAccountPage : Page
    {
        private readonly ISystemAccountServices _systemAccountServices;
        private ObservableCollection<SystemAccountResponseDTO> _accountsDTO;


        public ManageAccountPage(ISystemAccountServices systemAccountServices)
        {
            InitializeComponent();
            _systemAccountServices = systemAccountServices;
            LoadAccount();
        }
        private async void LoadAccount()
        {
            var account = await _systemAccountServices.getAllAsync();
            _accountsDTO = new ObservableCollection<SystemAccountResponseDTO>(account);
            AccountsDataGrid.ItemsSource = _accountsDTO;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
