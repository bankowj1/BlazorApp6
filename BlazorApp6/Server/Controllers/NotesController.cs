using AngleSharp.Dom;
using AngleSharp.Html;
using BlazorApp6.Server.Models;
using BlazorApp6.Server.Services.UserService;
using BlazorApp6.Shared.Models;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;

namespace BlazorApp6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class NotesController : ControllerBase
    {
        private readonly appdbContext _context;
        private readonly IUserService _userService;
        public static User user = new();
        HtmlSanitizer _sanitizer = new HtmlSanitizer();
        public NotesController(appdbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        [HttpGet, Authorize(Roles ="admin")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            var note = await _context.Notes
                .ToListAsync();

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }
        // GET: api/Notes/my
        [HttpGet("My")]
        public async Task<ActionResult<IEnumerable<Note>>> GetMyNotes()
        {
            var note = await _context.Notes.Where(n=> n.Users.Any(y=>y.Iduser == _userService.GetMyId())).ToListAsync();

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }
        [HttpPost]
        public async Task<ActionResult<Note>> PostNotes(NoteDTO note)
        {
           //testowane dziala
            Note note1 = new();
            note1.Note1 = Encoding.UTF8.GetBytes(_sanitizer.Sanitize(note.Note1));
            var l = new List<User>();
            var us = await _context.Users.FindAsync(_userService.GetMyId());
            if(us != null)
            l.Add(us);
            note1.Users = l;
            _context.Notes.Add(note1);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostNotes", new { id = note1.Idnotes }, note1);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, NoteDTO noteDTO)
        {
            Note note1 = new();
            note1.Note1 = Encoding.UTF8.GetBytes(_sanitizer.Sanitize(noteDTO.Note1));
            var note = await _context.Notes.FindAsync(id);
            bool amihere = false;
            foreach (var us in note.Users)
            {
                if(us.Iduser == _userService.GetMyId())
                {
                    amihere = true;
                }
            }
            if (amihere)
            {
                note1.Users = note.Users;
                note1.Idnotes = note.Idnotes;
                _context.Entry(note1).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        [HttpPost("adduser/{id,string}")]
        public async Task<IActionResult> PostAddUserNote(int id, string username)
        {
            //to zrobiłem dobrze wzoruj sie na tym jesli idzie o poczatek sanityzacje itp
            username = _sanitizer.Sanitize(username);
            Note note1 = new();
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
                return BadRequest();
            if (Amihere(id, note))
            {
                var user = await _context.Users.Where(us=> us.Username == username).SingleOrDefaultAsync();
                if (user != null)
                {
                    note1.Note1 = note.Note1;
                    ICollection<User> users = note.Users;
                    users.Add(user);
                    note1.Users = users;
                    note1.Idnotes = note.Idnotes;
                    _context.Entry(note1).State = EntityState.Modified;
                }
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotesExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return NoContent();
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatterial(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool NotesExists(int id)
        {
            return _context.Notes.Any(e => e.Idnotes == id);
        }
        private bool Amihere(int id,Note note)
        {
            bool amihere = false;
            foreach (var us in note.Users)
            {
                if (us.Iduser == _userService.GetMyId())
                {
                    amihere = true;
                }
            }
            return amihere;
        }
    }
}
