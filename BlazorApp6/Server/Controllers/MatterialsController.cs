using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MatterialsController : ControllerBase
    {
        private readonly appdbContext _context;

        public MatterialsController(appdbContext context)
        {
            _context = context;
        }

        // GET: api/Matterials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matterial>>> GetMatterials()
        {
            return await _context.Matterials.ToListAsync();
        }

        // GET: api/Matterials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Matterial>> GetMatterial(int id)
        {
            var matterial = await _context.Matterials.FindAsync(id);

            if (matterial == null)
            {
                return NotFound();
            }

            return matterial;
        }

        // PUT: api/Matterials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatterial(int id, Matterial matterial)
        {
            if (id != matterial.Idmat)
            {
                return BadRequest();
            }

            _context.Entry(matterial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatterialExists(id))
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

        // POST: api/Matterials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Matterial>> PostMatterial(Matterial matterial)
        {
            _context.Matterials.Add(matterial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatterial", new { id = matterial.Idmat }, matterial);
        }

        // DELETE: api/Matterials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatterial(int id)
        {
            var matterial = await _context.Matterials.FindAsync(id);
            if (matterial == null)
            {
                return NotFound();
            }

            _context.Matterials.Remove(matterial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatterialExists(int id)
        {
            return _context.Matterials.Any(e => e.Idmat == id);
        }
    }
}