using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.RegularExpressions;
using TimeAppRestApi.Models;

namespace TimeAppRestApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly string filePath = "group.json";
        private readonly string filePath2 = "users.json";
        public UserController UserController = new();

        [HttpGet]
        public IEnumerable<Team> Get()
        {
            var result = ReadTeamsFromFile();
            return result;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var team = ReadTeamsFromFile().FirstOrDefault(u => u.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        // Dodawanie nowej grupy
        [HttpPost]
        public IActionResult Post([FromBody] Team newTeam)
        {
            var teams = ReadTeamsFromFile();
            newTeam.Id = GetNextTeamId(teams);
            teams.Add(newTeam);
            WriteTeamsToFile(teams);

            var users = ReadUsersFromFile();
            var existingUser = users.FirstOrDefault(u => u.Id == newTeam.LeaderId);

            if (existingUser == null)
            {
                return NotFound();
            }
            existingUser.GroupsId.Add(newTeam.Id);

            WriteUsersToFile(users);

            return CreatedAtAction(nameof(Get), new { id = newTeam.Id }, newTeam);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeam(int id, [FromBody] Team updatedTeam)
        {
            var teams = ReadTeamsFromFile();
            var existingTeam = teams.FirstOrDefault(t => t.Id == id);

            if (existingTeam == null)
            {
                return NotFound();
            }

            // Aktualizacja danych drużyny
            existingTeam.Name = updatedTeam.Name;
            existingTeam.LeaderId = updatedTeam.LeaderId;
            existingTeam.Moderators = updatedTeam.Moderators;
            existingTeam.MembersIds = updatedTeam.MembersIds;
            existingTeam.TeamNotes = updatedTeam.TeamNotes;

            WriteTeamsToFile(teams);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            var teams = ReadTeamsFromFile();
            var team = teams.FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            teams.Remove(team);
            WriteTeamsToFile(teams);

            return NoContent();
        }

        [HttpPost("{id}/members")]
        public IActionResult AddMember(int id, [FromBody] int memberId)
        {
            var teams = ReadTeamsFromFile();
            var team = teams.FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            if (!team.MembersIds.Contains(memberId))
            {
                team.MembersIds.Add(memberId);
                WriteTeamsToFile(teams);
            }

            return NoContent();
        }

        [HttpDelete("{id}/members/{memberId}")]
        public IActionResult RemoveMember(int id, int memberId)
        {
            var teams = ReadTeamsFromFile();
            var team = teams.FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            team.MembersIds.Remove(memberId);
            WriteTeamsToFile(teams);

            return NoContent();
        }

        [HttpPost("{id}/notes")]
        public IActionResult AddNote(int id, [FromBody] TeamNote note)
        {
            var teams = ReadTeamsFromFile();
            var team = teams.FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            team.TeamNotes.Add(note);
            WriteTeamsToFile(teams);

            return NoContent();
        }

        [HttpDelete("{id}/notes/{noteId}")]
        public IActionResult RemoveNote(int id, int noteId)
        {
            var teams = ReadTeamsFromFile();
            var team = teams.FirstOrDefault(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            var note = team.TeamNotes.FirstOrDefault(n => n.Id == noteId);
            if (note != null)
            {
                team.TeamNotes.Remove(note);
                WriteTeamsToFile(teams);
            }

            return NoContent();
        }

        private int GetNextTeamId(List<Team> teams)
        {
            return teams.Any() ? teams.Max(u => u.Id) + 1 : 1;
        }


        // Metoda pomocnicza do odczytu danych grup z pliku
        private List<Team> ReadTeamsFromFile()
        {
            List<Team> teams = new List<Team>();

            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);
                teams = JsonSerializer.Deserialize<List<Team>>(json);
            }

            return teams;
        }

        private void WriteTeamsToFile(List<Team> teams)
        {
            string json = JsonSerializer.Serialize(teams);
            System.IO.File.WriteAllText(filePath, json);
        }
        // Metoda pomocnicza do odczytu danych użytkowników z pliku
        private List<User> ReadUsersFromFile()
        {
            List<User> users = new List<User>();

            if (System.IO.File.Exists(filePath2))
            {
                string json = System.IO.File.ReadAllText(filePath2);
                users = JsonSerializer.Deserialize<List<User>>(json);
            }

            return users;
        }

        // Metoda pomocnicza do zapisu danych użytkowników do pliku
        private void WriteUsersToFile(List<User> users)
        {
            string json = JsonSerializer.Serialize(users);
            System.IO.File.WriteAllText(filePath2, json);
        }
    }
}
