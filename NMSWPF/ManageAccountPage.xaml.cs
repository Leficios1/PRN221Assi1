using BusinessObject.Model;
using Services.DTO.Request;
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
using System.Windows.Forms;
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
            AccountDataGridViews.ItemsSource = _accountsDTO;
        }
        private void AccountDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(AccountDataGridViews.SelectedItem != null)
            {
                var selectAccount = (SystemAccountResponseDTO)AccountDataGridViews.SelectedItem;
                PopulateTextBoxes(selectAccount);
            }
            else
            {
                ClearTextBox();
            }
        }
        private void PopulateTextBoxes(SystemAccountResponseDTO account)
        {
            AccountIDTextBox.Text = account.AccountId.ToString();
            AccountNameTextBox.Text = account.AccountName;
            EmailTextBox.Text = account.AccountEmail;
            PasswordTextBox.Text = account.AccountPassword;

            foreach (ComboBoxItem item in RoleComboBox.Items)
            {
                if ((int)item.Tag == account.AccountRole)
                {
                    RoleComboBox.SelectedItem = item;
                    break;
                }
            }
        }
        private void ClearTextBox() 
        {
            AccountIDTextBox.Clear();
            AccountNameTextBox.Clear();
            EmailTextBox.Clear();
            PasswordTextBox.Clear();
            RoleComboBox.SelectedIndex = -1;
        }
        private void RoleComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            RoleComboBox.Items.Add(new ComboBoxItem { Content = "Staff", Tag = 1 });
            RoleComboBox.Items.Add(new ComboBoxItem { Content = "Lecture", Tag = 2 });
        }
        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dto = new SystemAccountRequestDTO
                {
                    AccountId = short.Parse(AccountIDTextBox.Text),
                    AccountName = AccountNameTextBox.Text,
                    AccountEmail = EmailTextBox.Text,
                    AccountPassword = PasswordTextBox.Text,
                    AccountRole = (int)((ComboBoxItem)RoleComboBox.SelectedItem).Tag
                };

                await _systemAccountServices.createAccount(dto);
                LoadAccount();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dto = new SystemAccountRequestDTO
                {
                    AccountId = short.Parse(AccountIDTextBox.Text),
                    AccountName = AccountNameTextBox.Text,
                    AccountEmail = EmailTextBox.Text,
                    AccountPassword = PasswordTextBox.Text,
                    AccountRole = (int)((ComboBoxItem)RoleComboBox.SelectedItem).Tag
                };
                await _systemAccountServices.updateAccount(dto);
                LoadAccount();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = System.Windows.MessageBox.Show("Are you sure you want to delete this account?", "Confirm Delete",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    await _systemAccountServices.deleteAccount(short.Parse(AccountIDTextBox.Text));
                    LoadAccount();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var adminWindow = new AdminWindow();
            adminWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void RoleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
