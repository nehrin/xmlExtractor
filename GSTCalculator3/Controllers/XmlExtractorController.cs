using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GSTCalculator.Model;
using Microsoft.AspNetCore.Mvc;

namespace GSTCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XmlExtractorController : ControllerBase
    {
        public ReservationContext ReservationContext { get; set; }

        public ExpenseContext ExpenseContext { get; set; }

        public XmlExtractorController(ExpenseContext expenseContext, ReservationContext reservationContext)
        {
            ReservationContext = reservationContext;

            if (reservationContext.ReservationItems.Count() == 0)
            {
                reservationContext.SaveChanges();
            }

            ExpenseContext = expenseContext;

            if (expenseContext.ExpenseItems.Count() == 0)
            {
                expenseContext.SaveChanges();
            }
        }

        [HttpPost]
        public async Task<List<object>> PostXmlExtractorAsync([FromBody] string value)
        {
            var extractionHelper = new ExtractionHelper.ExtractionHelper(value);
            List<IItem> items = extractionHelper.extractAsync();
            List<object> created_objects = new List<object>();

            foreach(IItem item in items){
                if (item.ItemType == ItemType.Expense)
                {
                    ExpenseContext.ExpenseItems.Add((ExpenseItem)item);
                    await ExpenseContext.SaveChangesAsync();

                    CreatedAtActionResult expense_create = CreatedAtAction(nameof(ExpenseController.GetExpenseItem), new { id = item.Id }, item);
                    created_objects.Add(expense_create.Value);
                }
                if(item.ItemType == ItemType.Reservation)
                {
                    ReservationContext.ReservationItems.Add((ReservationItem)item);
                    await ReservationContext.SaveChangesAsync();

                    CreatedAtActionResult reserv_create = CreatedAtAction(nameof(ReservationController.GetReservationItem), new { id = item.Id }, item);
                    created_objects.Add(reserv_create.Value);
                }
            }
            return created_objects;
        }
    }
}