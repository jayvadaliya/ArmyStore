using ArmyStore.Enums;

namespace ArmyStore.DataModels
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public DateTime UpdatedOn { get; set; }

        public Status Status { get; set; }
    }
}