﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp6.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsItemsController : ControllerBase
    {
        private readonly appdbContext _context;

        public MaterialsItemsController(appdbContext context)
        {
            _context = context;
        }

        // GET: api/MaterialsItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialsItem>>> GetMaterialsItems()
        {
            return await _context.MaterialsItems.ToListAsync();
        }

        // GET: api/MaterialsItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialsItem>> GetMaterialsItem(int id)
        {
            var materialsItem = await _context.MaterialsItems.FindAsync(id);

            if (materialsItem == null)
            {
                return NotFound();
            }

            return materialsItem;
        }

        // PUT: api/MaterialsItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterialsItem(int id, MaterialsItem materialsItem)
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
        public async Task<ActionResult<MaterialsItem>> PostMaterialsItem(MaterialsItem materialsItem)
        {
            _context.MaterialsItems.Add(materialsItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaterialsItem", new { id = materialsItem.IdmaterialsItem }, materialsItem);
        }

        // DELETE: api/MaterialsItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterialsItem(int id)
        {
            var materialsItem = await _context.MaterialsItems.FindAsync(id);
            if (materialsItem == null)
            {
                return NotFound();
            }

            _context.MaterialsItems.Remove(materialsItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaterialsItemExists(int id)
        {
            return _context.MaterialsItems.Any(e => e.IdmaterialsItem == id);
        }
    }
}