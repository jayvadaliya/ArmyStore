using ArmyStore.Enums;

namespace ArmyStore.Entities
{
    public class Product
    {
        private Product(
            int id,
            string name,
            decimal price,
            string imageUrl)
        {
            Id = id;
            Name = name;
            Price = price;
            ImageUrl = imageUrl;
            Status = Status.Available;
            UpdatedOn = System.DateTime.Now;

            ProductMetadata = ProductMetadata.Create(id, null, null);
        }

        internal Product(
            int id,
            string name,
            decimal price,
            string imageUrl,
            Status status,
            DateTime updatedOn)
            : this(id, name, price, imageUrl)
        {
            Status = status;
            UpdatedOn = updatedOn;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public string ImageUrl { get; private set; }

        public DateTime UpdatedOn { get; private set; }

        public Status Status { get; private set; }

        public ProductMetadata ProductMetadata { get; private set; }

        public static Product Create(int id, string name, decimal price, string imageUrl)
        {
            return new Product(id, name, price, imageUrl);
        }

        public Product UpdateImage(string imageUrl)
        {
            ImageUrl = imageUrl;
            Update();
            return this;
        }

        public Product LinkMetadata(ProductMetadata metadata)
        {
            if (metadata == null)
                return this;

            ProductMetadata = metadata;
            Update();
            return this;
        }

        public Product Update()
        {
            UpdatedOn = DateTime.Now;
            return this;
        }
    }
}