using CosmeticStoreLibrary.Models;
using CosmeticStoreLibrary.Services;
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

namespace CosmeticStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        private readonly UserService _userService;

        public AuthorizationPage()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ErrorMessageTextBlock.Text = "Пожалуйста, введите логин и пароль.";
                return;
            }

            User user = await _userService.AuthenticateUserAsync(login, password);

            if (user != null)
            {
                //  Сохраняем данные пользователя (в данном примере в свойство Application)
                Application.Current.Properties["CurrentUser"] = user;
                NavigationService?.Navigate(new ShopPage());
            }
            else
            {
                ErrorMessageTextBlock.Text = "Неверный логин или пароль.";
            }
        }
    }
}
