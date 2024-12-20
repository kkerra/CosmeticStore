using CosmeticStoreLibrary.Models;
using CosmeticStoreLibrary.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CosmeticStore.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreateOrderPage.xaml
    /// </summary>
    public partial class AdministrationPage : Page
    {
        private readonly OrderService _orderService = new();
        private readonly UserService _userService = new();
        private Order _currentOrder;

        public AdministrationPage()
        {
            InitializeComponent();
        }

        private async void SearchOrder_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(OrderIdTextBox.Text, out int orderId))
            {
                try
                {
                    _currentOrder = await _orderService.GetOrderByIdAsync(orderId);
                    await DisplayOrderDetailsAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Заказ не найден", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Указан неверный номер заказа", "Неверный ввод", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async Task DisplayOrderDetailsAsync()
        {
            try
            {
                if (_currentOrder != null)
                {
                    OrderDateTextBlock.Text = _currentOrder.OrderDate.ToString("d");

                    DeliveryDatePicker.SelectedDate = _currentOrder.OrderDeliveryDate;

                    StatusComboBox.SelectedItem = StatusComboBox.Items.Cast<ComboBoxItem>()
                        .FirstOrDefault(item => item.Content.ToString() == _currentOrder.OrderStatus);

                    if (_currentOrder.UserId != null)
                    {
                        var user = await _userService.GetUserByIdAsync(_currentOrder.UserId.Value);
                        CustomerNameTextBlock.Text = user?.Name ?? "N/A";
                    }
                    else
                    {
                        CustomerNameTextBlock.Text = "N/A";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }

        }

        private async void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (_currentOrder != null)
            {
                try
                {
                    _currentOrder.OrderDeliveryDate = DeliveryDatePicker.SelectedDate.Value;
                    _currentOrder.OrderStatus = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                    await _orderService.UpdateOrderAsync(_currentOrder);
                    MessageBox.Show("Данные заказа обновлены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка при сохранении", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Не загружено ни одного заказа для сохранения изменений.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = Application.Current.Properties["CurrentUser"] as User;
            NavigationService.Navigate(new CosmeticStore.Pages.ShopPage());
        }
    }
}
