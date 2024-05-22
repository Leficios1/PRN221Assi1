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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly AdminAccount _adminAccount;
        private readonly ISystemAccountServices _systemAccountServices;

        public Login()
        {
            InitializeComponent();
            _systemAccountServices = ((App)Application.Current).ServiceProvider.GetRequiredService<ISystemAccountServices>();
        }
        public Login(AdminAccount adminAccount) :this()
        {
            _adminAccount = adminAccount;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var email = EmailTextBox.Text;
            var password = PasswordBox.Password;

            if (_adminAccount.Email.Equals(email) && _adminAccount.Password.Equals(password))
            {
                MessageBox.Show("Admin Login Successful");
                var adminWindow = new AdminWindow();
                adminWindow.Show();
                this.Close();
                return;
            }

            if (await _systemAccountServices.Login(email, password))
            {
                MessageBox.Show("Staff Login Successful");
                var accountName = await _systemAccountServices.getAccountName(email);
                var staffWindow = new StaffWindow();
                staffWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Email or Password");
            }
        }


        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
