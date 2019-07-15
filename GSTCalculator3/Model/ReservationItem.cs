using GSTCalculator.Model;
using System;

namespace GSTCalculator.Model
{
    public class ReservationItem : IItem
    {
        public int Id { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public DateTime ReservationTime { get; set; }
        public ItemType ItemType { get { return ItemType.Reservation; } }
    }
}
