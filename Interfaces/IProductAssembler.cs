using ArmyStore.Dtos;
using ArmyStore.Entities;

namespace ArmyStore.Interfaces
{
    public interface IProductAssembler
    {
        Product CreateEntity(InsertProductDto dto);

        IEnumerable<ProductDto> WriteDtos(IEnumerable<Product> products);

        ProductDto WriteDto(Product entity);
    }
}