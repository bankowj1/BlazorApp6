using BlazorApp6.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlazorApp6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MaterialsItemsController : ControllerBase
    {
        private readonly appdbContext _context;

        public MaterialsItemsController(appdbContext context)
        {
            _context = context;
        }

        // GET: api/MaterialsItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemsMatterial>>> GetMaterialsItems()
        {
            return await _context.ItemsMatterials.ToListAsync();
        }

        // GET: api/MaterialsItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemsMatterial>> GetMaterialsItem(int id)
        {
            var materialsItem = await _context.ItemsMatterials.FindAsync(id);

            if (materialsItem == null)
            {
                return NotFound();
            }

            return materialsItem;
        }
        [HttpGet("Details/{id}")]
        public async Task<ActionResult<ItemsMatterial>> GetMaterialsItemDetails(int id)
        {
            var materialsItem = await _context.ItemsMatterials.FindAsync(id);

            if (materialsItem == null)
            {
                return NotFound();
            }
            await _context.Entry(materialsItem)
                .Reference(materialsItem => materialsItem.Mat)
                .LoadAsync();
            await _context.Entry(materialsItem)
                .Reference(materialsItem => materialsItem.Item)
                .LoadAsync();
            return materialsItem;
        }
        [HttpGet("Item/{id}")]
        public async Task<ActionResult<List<ItemsMatterial>>> GetItemsGroupOfItem(int id)
        {
            var materialsItem = await _context.ItemsMatterials.Where(mi => mi.ItemId == id).Include(ig => ig.Mat).ToListAsync();

            if (materialsItem == null)
            {
                return NotFound();
            }
            return materialsItem;
        }

        [HttpGet("Mat/{id}")]
        public async Task<ActionResult<List<ItemsMatterial>>> GetItemsGroupOfMat(int id)
        {
            var materialsItem = await _context.ItemsMatterials.Where(mi => mi.MatId == id).Include(ig => ig.Item).Include(ig => ig.Mat).ToListAsync();

            if (materialsItem == null)
            {
                return NotFound();
            }

            return materialsItem;
        }

        // PUT: api/MaterialsItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterialsItem(int id, ItemsMatterial materialsItem)
        {
            if (id != materialsItem.IdmaterialsItem)
            {
                return BadRequest();
            }

            _context.Entry(materialsItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialsItemExists(id))
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

        // POST: api/MaterialsItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemsMatterial>> PostMaterialsItem(ItemsMatterial materialsItem)
        {
            _context.ItemsMatterials.Add(materialsItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaterialsItem", new { id = materialsItem.IdmaterialsItem }, materialsItem);
        }

        // DELETE: api/MaterialsItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterialsItem(int id)
        {
            var materialsItem = await _context.ItemsMatterials.FindAsync(id);
            if (materialsItem == null)
            {
                return NotFound();
            }

            _context.ItemsMatterials.Remove(materialsItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaterialsItemExists(int id)
        {
            return _context.ItemsMatterials.Any(e => e.IdmaterialsItem == id);
        }
    }
}