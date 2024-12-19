using CosmeticStoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticStoreLibrary.Services
{
    public class OrderService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "http://localhost:5196/api/";

        public OrderService()
        {
            _client = new HttpClient { BaseAddress = new Uri(_baseUrl) };
        }
        public async Task CreateOrderAsync(List<string> productIds)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("Orders/createOrder", new { ProductIds = productIds });
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка HTTP запроса: {ex.Message}");
            }
        }
        public async Task<List<Order>> GetOrdersAsync()
        {
            try
            {
                var response = await _client.GetAsync("Orders/getOrders");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<Order>>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка HTTP запроса: {ex.Message}");
                return new List<Order>();
            }
        }
    }
}
