using BlazorApp6.Server.Services.UserService;
using BlazorApp6.Shared.Models;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
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
        private HtmlSanitizer _sanitizer = new HtmlSanitizer();

        public NotesController(appdbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet, Authorize(Roles = "admin")]
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

        [HttpGet("{id}"), Authorize(Roles = "admin")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }
            await _context.Entry(note)
                .Collection<User>(note => note.Users)
                .LoadAsync();
            return note;
        }

        // GET: api/Notes/My
        [HttpGet("My")]
        public async Task<ActionResult<IEnumerable<Note>>> GetMyNotes()
        {
            var note = await _context.Notes.Where(n => n.Users.Any(y => y.Iduser == _userService.GetMyId())).ToListAsync();

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
            if (us != null)
                l.Add(us);
            note1.Users = l;
            note1.IsCoded = (bool)note.IsCoded;
            _context.Notes.Add(note1);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost("encode")]
        public async Task<byte[]> PostEncodeNotes(NoteDTOEnco note)
        {
            if (note.Note1 != null && note.pass != null)
            {
                byte[] passwordArray = Encoding.UTF8.GetBytes(note.pass);
                byte[] encoded;
                // Use the password to generate the key and initialization vector
                byte[] key, iv;
                using (var deriveBytes = new Rfc2898DeriveBytes(note.pass, passwordArray))
                {
                    key = deriveBytes.GetBytes(32);
                    iv = deriveBytes.GetBytes(16);
                }
                using (Aes myAes = Aes.Create())
                {
                    myAes.Key = key;
                    myAes.IV = iv;
                    ICryptoTransform encryptor = myAes.CreateEncryptor(myAes.Key, myAes.IV);

                    // Create the streams used for encryption.
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write((string)note.Note1);
                            }
                            encoded = msEncrypt.ToArray();
                        }
                    }
                }
                return encoded;

            }
            throw new Exception("bad password or empty note");

        }
        [HttpPost("decode")]
        public async Task<string> PostDecodeNotes(NoteDTODeco note)
        {
            if (note.Note1 != null && note.pass != null)
            {
                byte[] passwordArray = Encoding.UTF8.GetBytes(note.pass);
                
                string decoded = string.Empty;
                // Use the password to generate the key and initialization vector
                byte[] key, iv;
                using (var deriveBytes = new Rfc2898DeriveBytes(note.pass, passwordArray))
                {
                    key = deriveBytes.GetBytes(32);
                    iv = deriveBytes.GetBytes(16);
                }
                using (Aes myAes = Aes.Create())
                {
                    myAes.Key = key;
                    myAes.IV = iv;
                    ICryptoTransform decryptor = myAes.CreateDecryptor(myAes.Key, myAes.IV);

                    // Create the streams used for encryption.
                    using (MemoryStream msDecrypt = new MemoryStream(note.Note1))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {

                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                decoded = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                return decoded;
            }
            throw new Exception("bad password or empty note");

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNoteBytes(int id, NoteDTOBytes noteDTO)
        {
            Note note1 = new();
            note1.Note1 = noteDTO.Note1;
            var note = await _context.Notes.Include(n => n.Users).SingleOrDefaultAsync(n => n.Idnotes == id);
            if (note == null)
                return NotFound();

            if (Amihere(note))
            {
                note.Note1 = note1.Note1;
                note.IsCoded = (bool)noteDTO.IsCoded;
                _context.Entry(note).State = EntityState.Modified;
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
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("Bytes/{id}")]
        public async Task<IActionResult> PutNote(int id, NoteDTO noteDTO)
        {
            Note note1 = new();
            note1.Note1 = Encoding.UTF8.GetBytes(_sanitizer.Sanitize(noteDTO.Note1));
            var note = await _context.Notes.Include(n => n.Users).SingleOrDefaultAsync(n => n.Idnotes == id);
            if (note == null)
                return NotFound();

            if (Amihere(note))
            {
                note.Note1 = note1.Note1;
                note.IsCoded = (bool)noteDTO.IsCoded;
                _context.Entry(note).State = EntityState.Modified;
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
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("adduser/{id}")]
        public async Task<IActionResult> PutAddUserNote(int id ,AddNoteUserDTO noteUserDTO)
        {
            //to zrobiłem dobrze wzoruj sie na tym jesli idzie o poczatek sanityzacje itp
            noteUserDTO.UserName = _sanitizer.Sanitize(noteUserDTO.UserName);
            Note note1 = new();
            var note = await _context.Notes.Include(n => n.Users).SingleOrDefaultAsync(n => n.Idnotes == noteUserDTO.NoteID);
            if (note == null)
                return BadRequest();
            if (Amihere(note))
            {
                var user = await _context.Users.Where(us => us.Username == noteUserDTO.UserName).SingleOrDefaultAsync();
                if (user != null)
                {
                    ICollection<User> users = note.Users;
                    users.Add(user);
                    note.Users = users;
                    _context.Entry(note).State = EntityState.Modified;
                }
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotesExists(noteUserDTO.NoteID))
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
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await _context.Notes.Include(n => n.Users).SingleOrDefaultAsync(n => n.Idnotes == id);

            if (note == null)
            {
                return NotFound();
            }
            if (Amihere(note))
            {
                foreach (var user in note.Users)
                {
                    user.Notes.Remove(note);
                }
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        private bool NotesExists(int id)
        {
            return _context.Notes.Any(e => e.Idnotes == id);
        }

        private bool Amihere(Note note)
        {
            bool amihere = false;

            foreach (var us in note.Users)
            {
                if (us.Iduser == _userService.GetMyId())
                {
                    amihere = true;
                }
            }
            Console.WriteLine(amihere.ToString());
            return amihere;
        }
    }
}