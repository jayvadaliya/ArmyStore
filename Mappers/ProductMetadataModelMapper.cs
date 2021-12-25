using ArmyStore.DataModels;
using ArmyStore.Entities;
using ArmyStore.Interfaces;

namespace ArmyStore.Mappers
{
    public class ProductMetadataModelMapper : IMapper<ProductMetadata, ProductMetadataModel>
    {
        public ProductMetadataModel MapToDataModel(ProductMetadata source)
        {
            if (source == null)
                return null;

            return new ProductMetadataModel
            {
                Id = source.Id,
                Description = source.Description,
                Specifications = source.Specifications
            };
        }

        public ProductMetadata MapToDomain(ProductMetadataModel source)
        {
            if (source == null)
                return null;

            return ProductMetadata.Create(
                source.Id,
                source.Description,
                source.Specifications);
        }
    }
}