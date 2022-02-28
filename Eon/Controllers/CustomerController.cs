using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eon.Models;
using Microsoft.EntityFrameworkCore;

namespace Eon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ShopDbContext context;
        public CustomerController(ShopDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await context.Customers.Select(e => e).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var Customer = await context.Customers.FindAsync(id);

            if (Customer == null)
            {
                return NotFound();
            }

            return Customer;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer Customer)
        {
            context.Customers.Add(Customer);
            await context.SaveChangesAsync();
            return CreatedAtAction("GetCustomer",
            new { id = Customer.CustomerId }, Customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer Customer)
        {
            if (id != Customer.CustomerId)
            {
                return BadRequest();
            }

            context.Entry(Customer).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var Customer = await context.Customers.FindAsync(id);
            if (Customer == null)
            {
                return NotFound();
            }
            context.Customers.Remove(Customer);
            await context.SaveChangesAsync();
            return Customer;
        }

        private bool CustomerExists(int id)
        {
            return context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
