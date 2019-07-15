using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GSTCalculator;
using GSTCalculator.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GSTCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationContext _context;
        public ReservationController(ReservationContext context)
        {
            _context = context;

            if (_context.ReservationItems.Count() == 0)
            {
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationItem>>> GetReservationItems()
        {
            return await _context.ReservationItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationItem>> GetReservationItem(int id)
        {
            var reservationItem = await _context.ReservationItems.FindAsync(id);

            if (reservationItem == null)
            {
                return NotFound();
            }

            return reservationItem;
        }

        [HttpPost]
        public async Task<ActionResult<ReservationItem>> PostReservationItem(ReservationItem item)
        {
            _context.ReservationItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReservationItem), new { id = item.Id }, item);
        }
    }
}