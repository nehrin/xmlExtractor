using GSTCalculator.Model;
using Microsoft.EntityFrameworkCore;

namespace GSTCalculator
{
    public class ExpenseContext : DbContext
    {
        public ExpenseContext(DbContextOptions<ExpenseContext> options)
            : base(options)
        {
        }

        public DbSet<ExpenseItem> ExpenseItems { get; set; }
    }
}
