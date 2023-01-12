using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlazorApp6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemsGroupsController : ControllerBase
    {
        private readonly appdbContext _context;

        public ItemsGroupsController(appdbContext context)
        {
            _context = context;
        }

        // GET: api/ItemsGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemsGroup>>> GetItemsGroups()
        {
            return await _context.ItemsGroups.ToListAsync();
        }

        // GET: api/ItemsGroups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemsGroup>> GetItemsGroup(int id)
        {
            var itemsGroup = await _context.ItemsGroups.FindAsync(id);

            if (itemsGroup == null)
            {
                return NotFound();
            }

            return itemsGroup;
        }
        // GET: api/ItemsGroups/5
        [HttpGet("Details/{id}")]
        public async Task<ActionResult<ItemsGroup>> GetItemsGroupDetails(int id)
        {
            var itemsGroup = await _context.ItemsGroups.FindAsync(id);

            if (itemsGroup == null)
            {
                return NotFound();
            }
            await _context.Entry(itemsGroup)
                .Reference(itemsGroup => itemsGroup.Group)
                .LoadAsync();
            await _context.Entry(itemsGroup)
                .Reference(itemsGroup => itemsGroup.Item)
                .LoadAsync();

            return itemsGroup;
        }
        [HttpGet("Group/{id}")]
        public async Task<ActionResult<IEnumerable<ItemsGroup>>> GetItemsGroupOfGroup(int id)
        {
            var itemsGroup = await _context.ItemsGroups.Where(ig => ig.GroupId == id).Include(ig => ig.Item).Include(ig => ig.Group).ToListAsync();

            if (itemsGroup == null)
            {
                return NotFound();
            }

            return itemsGroup;
        }

        [HttpGet("Item/{id}")]
        public async Task<ActionResult<IEnumerable<ItemsGroup>>> GetItemsGroupOfItem(int id)
        {
            var itemsGroup = await _context.ItemsGroups.Where(ig => ig.ItemId == id).Include(ig => ig.Item).Include(ig => ig.Group).ToListAsync();

            if (itemsGroup == null)
            {
                return NotFound();
            }

            return itemsGroup;
        }

        // PUT: api/ItemsGroups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemsGroup(int id, ItemsGroup itemsGroup)
        {
            if (id != itemsGroup.IditemsGroup)
            {
                return BadRequest();
            }

            _context.Entry(itemsGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemsGroupExists(id))
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

        // POST: api/ItemsGroups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemsGroup>> PostItemsGroup(ItemsGroup itemsGroup)
        {
            _context.ItemsGroups.Add(itemsGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemsGroup", new { id = itemsGroup.IditemsGroup }, itemsGroup);
        }

        // DELETE: api/ItemsGroups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemsGroup(int id)
        {
            var itemsGroup = await _context.ItemsGroups.FindAsync(id);
            if (itemsGroup == null)
            {
                return NotFound();
            }

            _context.ItemsGroups.Remove(itemsGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemsGroupExists(int id)
        {
            return _context.ItemsGroups.Any(e => e.IditemsGroup == id);
        }
    }
}