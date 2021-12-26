using ArmyStore.Dtos;
using ArmyStore.Entities;
using ArmyStore.Interfaces;

namespace ArmyStore.Assembler
{
    public class ProductAssembler : IProductAssembler
    {
        public Product CreateEntity(InsertProductDto dto)
        {
            if (dto == null)
                return null;

            var product = Product.Create(0, dto.Name, dto.Price, dto.ImageUrl);
            if (!string.IsNullOrWhiteSpace(dto.Description) || !string.IsNullOrWhiteSpace(dto.Specifications))
            {
                var metadata = ProductMetadata.Create(0, dto.Description, dto.Specifications);
                product.LinkMetadata(metadata);
            }

            return product;
        }

        public IEnumerable<ProductDto> WriteDtos(IEnumerable<Product> products)
        {
            if (products == null && !products.Any())
                return Enumerable.Empty<ProductDto>();

            var dtos = new List<ProductDto>();
            foreach (var product in products)
                dtos.Add(WriteDto(product));

            return dtos;
        }

        public ProductDto WriteDto(Product entity)
        {
            if (entity == null)
                return null;

            var dto = new ProductDto();
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.Price = entity.Price;
            dto.Status = entity.Status;
            dto.UpdatedOn = entity.UpdatedOn;
            dto.ImageUrl = entity.ImageUrl;
            
            if (entity.ProductMetadata != null)
            {
                dto.Metadata = new ProductMetadataDto()
                {
                    Description = entity.ProductMetadata.Description,
                    Specifications = entity.ProductMetadata.Specifications
                };
            }

            return dto;
        }
    }
}