using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Eon.Models;
using Microsoft.EntityFrameworkCore;

namespace Eon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ShopDbContext context;

        public ItemController(ShopDbContext context)
        {
            this.context = context;
        }

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetItems()
        {
            return await context.Items.Include(i => i.Owner).ToListAsync();
        }

        // GET: api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Item>>> GetItem(int id)
        {
            var item = await context.Items
                .Where(i => i.ItemId == id)
                .Include(i => i.Owner)
                .ToListAsync();

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/items/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.ItemId)
            {
                return BadRequest();
            }

            context.Entry(item).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/items
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            context.Items.Add(item);
            if (item.CustomerId != null || item.CustomerId != 0)
                context.Customers.Where(c => c.CustomerId == item.CustomerId).FirstOrDefault().CustomerWealth += item.ItemPrice;
            await context.SaveChangesAsync();
            
            return CreatedAtAction("GetItem", new { id = item.ItemId }, item);
        }

        // DELETE: api/items/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> DeleteItem(int id)
        {
            var item = await context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            context.Items.Remove(item);
            await context.SaveChangesAsync();

            return item;
        }

        private bool ItemExists(int id)
        {
            return context.Items.Any(e => e.ItemId == id);
        }
    }
}
