namespace ArmyStore.Entities
{
    public class ProductMetadata
    {
        private ProductMetadata(long id, string description, string specs)
        {
            Id = id;
            Description = description;
            Specifications = specs;
        }

        public long Id { get; private set; }

        public string Description { get; private set; }

        public string Specifications { get; private set; }

        public static ProductMetadata Create(long id, string description, string specs)
        {
            return new ProductMetadata(id, description, specs);
        }

        public ProductMetadata Update(string description, string specifications)
        {
            Description = description;
            Specifications = specifications;
            return this;
        }

        public ProductMetadata LinkProductId(long id)
        {
            if (id < 0)
                throw new ArgumentException("Invalid value of Id.");

            Id = id;
            return this;
        }
    }
}