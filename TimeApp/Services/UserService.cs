using TimeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Json;
using Isopoh.Cryptography.Argon2;
using TimeAppRestApi.Models;

namespace TimeApp.Services
{
    public class UserService
    {
        HttpClient _httpClient;

        String httpadres = "https://timeapprestapi20231206171140.azurewebsites.net/";

        public UserService()
        {
            _httpClient = new HttpClient();
        }

        User user = new();
        public async Task<User> GetUser(String email)
        {
            var url = httpadres + "/api/User/byemail/" + email;
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadFromJsonAsync<User>();

            }
            return user;
        }
        public async Task<User> GetUserById(int id)
        {
            var url = httpadres + "/api/User/" + id;
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadFromJsonAsync<User>();

            }
            return user;
        }

        public async Task<bool> AddUser(User user)
        {
            var response = await _httpClient.PostAsJsonAsync(httpadres + "/api/User", user);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var response = await _httpClient.PutAsJsonAsync(httpadres + "/api/User/" + user.Id.ToString(), user);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> AddNote(Note note)
        {
            var userId = App.User.Id;
            var response = await _httpClient.PostAsJsonAsync(httpadres + "/api/User/" + userId + "/Notes" , note);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteNote(int id)
        {
            var userId = App.User.Id;
            var response = await _httpClient.DeleteAsync(httpadres + "/api/User/" + userId + "/Notes/" + id);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
