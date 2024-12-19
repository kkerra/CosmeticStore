using CosmeticStoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticStoreLibrary.Services
{
    public class UserService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "http://localhost:5196/api/";

        public UserService()
        {
            _client = new() { BaseAddress = new Uri(_baseUrl) };
        }

        public async Task<User> AuthenticateUserAsync(string login, string password)
        {
            try
            {
                var response = await _client.GetAsync($"Users/login?login={login}&password={password}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<User>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка HTTP запроса: {ex.Message}");
                return null;
            }
        }
    }
}
