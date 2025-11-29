namespace Domain.Entities
{
    public enum FoodType { Breakfast, Lunch }
    public class Food
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public FoodType Type { get; set; }
        public decimal Price { get; set; }
        public int Calories { get; set; }
        public bool Available { get; set; }
        public List<FoodMenu>? FoodMenus { get; set; }
    }
}
