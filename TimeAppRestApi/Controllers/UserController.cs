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

        // Usuwanie istniejącego użytkownika
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var users = ReadUsersFromFile();
            var userToDelete = users.FirstOrDefault(u => u.Id == id);

            if (userToDelete == null)
            {
                return NotFound();
            }

            users.Remove(userToDelete);
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

        [HttpPost("{id}/notes")]
        public IActionResult AddNote(int id, [FromBody] Note newNote)
        {
            var users = ReadUsersFromFile();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            if (user.Notes == null)
            {
                user.Notes = new List<Note>();
            }

            newNote.Id = GetNextNoteId(user.Notes);
            user.Notes.Add(newNote);

            WriteUsersToFile(users);

            return CreatedAtAction(nameof(GetNotes), new { id = user.Id }, user.Notes);
        }

        [HttpPut("{id}/notes/{noteId}")]
        public IActionResult UpdateNote(int id, int noteId, [FromBody] Note updatedNote)
        {
            var users = ReadUsersFromFile();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null || user.Notes == null)
            {
                return NotFound();
            }

            var existingNote = user.Notes.FirstOrDefault(n => n.Id == noteId);

            if (existingNote == null)
            {
                return NotFound();
            }

            // Zaktualizuj dane notatki
            existingNote.Title = updatedNote.Title;
            existingNote.Description = updatedNote.Description;
            existingNote.StartDate = updatedNote.StartDate;
            existingNote.Deadline = updatedNote.Deadline;
            existingNote.Status = updatedNote.Status;

            WriteUsersToFile(users);

            return NoContent();
        }

        [HttpDelete("{id}/notes/{noteId}")]
        public IActionResult DeleteNote(int id, int noteId)
        {
            var users = ReadUsersFromFile();
            var user = users.FirstOrDefault(u => u.Id == id);

            if (user == null || user.Notes == null)
            {
                return NotFound();
            }

            var noteToDelete = user.Notes.FirstOrDefault(n => n.Id == noteId);

            if (noteToDelete == null)
            {
                return NotFound();
            }

            user.Notes.Remove(noteToDelete);

            WriteUsersToFile(users);

            return NoContent();
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