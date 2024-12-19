using CosmeticStoreLibrary.Models;
using CosmeticStoreLibrary.Services;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;

namespace CosmeticStore.ViewModels
{
    class ShopPageViewModel
    {
        private readonly ProductService _productService;
        public ObservableCollection<Product> Products { get; private set; }

        public ShopPageViewModel()
        {
            _productService = new ProductService();
            Products = new ObservableCollection<Product>();
            LoadProductsAsync();
        }

        private async void LoadProductsAsync()
        {
            try
            {
                var products = await _productService.GetProductsAsync();
                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
            }
        }
    }
}
