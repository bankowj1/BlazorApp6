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
            HtmlSanitizer _sanitizer = new HtmlSanitizer();
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
    }
}
