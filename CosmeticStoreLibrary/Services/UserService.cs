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
                string responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response: {responseString}");

                var user = await response.Content.ReadFromJsonAsync<User>();
                return user;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Ошибка HTTP запроса: {ex.Message}");
                return null;
            }
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var response = await _client.GetAsync($"Users/{userId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<User>();
        }
    }
}
