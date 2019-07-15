using GSTCalculator.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSTCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseContext _context;

        public ExpenseController(ExpenseContext context)
        {
            _context = context;

            if (_context.ExpenseItems.Count() == 0)
            {
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseItem>>> GetExpenseItems()
        {
            return await _context.ExpenseItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseItem>> GetExpenseItem(int id)
        {
            var expenseItem = await _context.ExpenseItems.FindAsync(id);

            if (expenseItem == null)
            {
                return NotFound();
            }

            return expenseItem;
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseItem>> PostExpenseItem(ExpenseItem item)
        {
            _context.ExpenseItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpenseItem), new { id = item.Id }, item);
        }
    }
}