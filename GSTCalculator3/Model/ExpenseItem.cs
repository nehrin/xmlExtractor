
using GSTCalculator.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GSTCalculator.Model
{
    public class ExpenseItem : IItem
    {
        public int Id { get; set; }
        public string CostCentre { get; set; }
        public double? Total { get; set; }
        public string PaymentMethod { get; set; }
        public ItemType ItemType { get { return ItemType.Expense; } }

        [NotMapped]
        public double GST
        {
            get
            {
                var calculator = new GSTCalculator();
                return Math.Round(calculator.CalculateGST(Total), 2);
            }
        }

        public double TotalExcludingGST
        {
            get
            {
                var calculator = new GSTCalculator();
                return Math.Round(calculator.CalculateTotalExcludeingGST(Total), 2);
            }
        }
    }
}
