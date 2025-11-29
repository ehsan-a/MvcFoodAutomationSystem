namespace Domain.Entities
{
    public class FoodMenu
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public virtual Menu? Menu { get; set; }
        public int FoodId { get; set; }
        public virtual Food? Food { get; set; }
        public decimal UnitPrice { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<Reservation>? Reservations { get; set; }
    }
}
