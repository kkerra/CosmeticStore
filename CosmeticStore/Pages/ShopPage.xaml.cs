﻿using CosmeticStoreLibrary.Models;
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
    /// Логика взаимодействия для ShopPage.xaml
    /// </summary>
    public partial class ShopPage : Page
    {
        public ShopPage()
        {
            InitializeComponent();
            var currentUser = Application.Current.Properties["CurrentUser"] as User;
            if (currentUser != null)
            {
                FullNameTextBlock.Text = $"{currentUser.Surname} {currentUser.Name} {currentUser.Patronymic}";
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["CurrentUser"] = null;
            NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
