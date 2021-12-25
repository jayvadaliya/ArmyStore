namespace ArmyStore.Entities
{
    public class ProductMetadata
    {
        private ProductMetadata(int id, string description, string specs)
        {
            Id = id;
            Description = description;
            Specifications = specs;
        }

        public int Id { get; private set; }

        public string Description { get; private set; }

        public string Specifications { get; private set; }

        public static ProductMetadata Create(int id, string description, string specs)
        {
            return new ProductMetadata(id, description, specs);
        }

        public ProductMetadata Update(string description, string specifications)
        {
            Description = description;
            Specifications = specifications;
            return this;
        }
    }
}