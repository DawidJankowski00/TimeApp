using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TimeApp.Models;

namespace TimeApp.Services
{
    public class TeamService
    {
        HttpClient _httpClient;

        String httpadres = "https://timeapprestapi20231206171140.azurewebsites.net/";

        public TeamService()
        {
            _httpClient = new HttpClient();
        }

        Team team = new();
        public async Task<Team> GetTeam(int id)
        {

            var url = httpadres + "/api/Group/" + id;
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                team = await response.Content.ReadFromJsonAsync<Team>();
            }
            return team;
        }

        public async Task<bool> AddTeam(Team team)
        {
            var url = httpadres + "/api/Group/";
            var response = await _httpClient.PostAsJsonAsync(url, team);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateTeam(Team team)
        {
            var response = await _httpClient.PutAsJsonAsync(httpadres + "/api/Group/" + team.Id.ToString(), team);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
