using AutoMapper;
using BusinessObject.Model;
using Microsoft.Extensions.DependencyInjection;
using NMSWPF;
using Services.DTO.Request;
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
    /// Interaction logic for ManageStaffAccountPage.xaml
    /// </summary>
    public partial class ManageStaffAccountPage : Page
    {
        private readonly ISystemAccountServices _systemAccountServices;
        private readonly SystemAccount _systemAccount;
        private readonly IMapper _mapper;
        public ManageStaffAccountPage(ISystemAccountServices systemAccountServices, SystemAccount systemAccount)
        {
            InitializeComponent();
            _systemAccountServices = systemAccountServices;
            _systemAccount = systemAccount;
            _mapper = ((App)Application.Current).ServiceProvider.GetRequiredService<IMapper>();
            LoadAccount();
        }
        private async void LoadAccount()
        {
            AccountIdLabel.Content = _systemAccount.AccountId.ToString();
            NameTextBox.Text = _systemAccount.AccountName;
            EmailTextBox.Text = _systemAccount.AccountEmail;
            PasswordTextBox.Text = _systemAccount.AccountPassword;
            RoleComboBox.SelectedIndex = _systemAccount.AccountRole == 1 ? 0 : 1;
        }

        private void NewsArticlesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _systemAccount.AccountId = short.Parse(AccountIdLabel.Content.ToString());
                _systemAccount.AccountName = NameTextBox.Text;
                _systemAccount.AccountEmail = EmailTextBox.Text;
                _systemAccount.AccountPassword = PasswordTextBox.Text;

                var mapper = _mapper.Map<SystemAccountRequestDTO>(_systemAccount);
                await _systemAccountServices.updateAccount(mapper);
                MessageBox.Show("Update Successful");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
