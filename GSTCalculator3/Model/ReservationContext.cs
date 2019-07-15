using GSTCalculator.Model;
using Microsoft.EntityFrameworkCore;

namespace GSTCalculator
{
    public class ReservationContext : DbContext
    {
        public ReservationContext(DbContextOptions<ReservationContext> options)
            : base(options)
        {
        }

        public DbSet<ReservationItem> ReservationItems { get; set; }
    }
}