using ArmyStore.Enums;

namespace ArmyStore.Dtos
{
    public class ProductDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public DateTime UpdatedOn { get; set; }

        public Status Status { get; set; }

        public ProductMetadataDto Metadata { get; set; }
    }
}