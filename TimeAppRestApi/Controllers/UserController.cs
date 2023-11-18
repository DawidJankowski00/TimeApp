using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TimeAppRestApi.Models;

namespace TimeAppRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly string filePath = "users.json"; // Ścieżka do pliku z danymi użytkowników

        [HttpGet]
        public IEnumerable<User> Get()
        {
            var result = ReadUsersFromFile();
            return result;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = ReadUsersFromFile().FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("byemail/{email}")]
        public IActionResult GetByName(string email)
        {
            var user = ReadUsersFromFile().FirstOrDefault(u => Argon2.Verify(u.Email, email.ToLower()));

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // Dodawanie nowego użytkownika
        [HttpPost]
        public IActionResult Post([FromBody] User newUser)
        {
            var users = ReadUsersFromFile();
            newUser.Id = GetNextUserId(users);
            users.Add(newUser);
            WriteUsersToFile(users);

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User updatedUser)
        {
            var users = ReadUsersFromFile();
            var existingUser = users.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                return NotFound();
            }

            // Zaktualizuj dane użytkownika
            existingUser.Name = updatedUser.Name;
            existingUser.Email = updatedUser.Email;
            existingUser.Password = updatedUser.Password;
            existingUser.Notes = updatedUser.Notes;
            existingUser.GroupsId = updatedUser.GroupsId;

            WriteUsersToFile(users);

            return NoContent();
        }

        

        [HttpGet("{id}/notes")]
        public IActionResult GetNotes(int id)
        {
            var user = ReadUsersFromFile().FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.Notes);
        }

        

        

        // Metoda pomocnicza do odczytu danych użytkowników z pliku
        private List<User> ReadUsersFromFile()
        {
            List<User> users = new List<User>();

            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);
                users = JsonSerializer.Deserialize<List<User>>(json);
            }

            return users;
        }

        // Metoda pomocnicza do zapisu danych użytkowników do pliku
        private void WriteUsersToFile(List<User> users)
        {
            string json = JsonSerializer.Serialize(users);
            System.IO.File.WriteAllText(filePath, json);
        }

        // Metoda pomocnicza do uzyskania kolejnego dostępnego identyfikatora użytkownika
        private int GetNextUserId(List<User> users)
        {
            return users.Any() ? users.Max(u => u.Id) + 1 : 1;
        }

        private int GetNextNoteId(List<Note> notes)
        {
            return notes.Any() ? notes.Max(n => n.Id) + 1 : 1;
        }
    }
}