using ArmyStore.DataModels;
using ArmyStore.Entities;
using ArmyStore.Interfaces;

namespace ArmyStore.Mappers
{
    public class ProductDataModelMapper : IMapper<Product, ProductModel>
    {
        public ProductModel MapToDataModel(Product product)
        {
            if (product == null)
                return null;

            return new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Status = product.Status,
                ImageUrl = product.ImageUrl,
                UpdatedOn = product.UpdatedOn
            };
        }

        public Product MapToDomain(ProductModel productModel)
        {
            if (productModel == null)
                return null;

            return new Product(
                productModel.Id,
                productModel.Name,
                productModel.Price,
                productModel.ImageUrl,
                productModel.Status,
                productModel.UpdatedOn);
        }
    }
}