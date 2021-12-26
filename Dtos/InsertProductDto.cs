using ArmyStore.Enums;

namespace ArmyStore.Dtos
{
    public class InsertProductDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public DateTime UpdatedOn { get; set; }

        public Status Status { get; set; }

        public string Description { get; set; }

        public string Specifications { get; set; }
    }
}