using ArmyStore.Entities;

namespace ArmyStore.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> Create(Product product);

        Task<Product> GetProduct(int id);

        Task<IEnumerable<Product>> GetList();
    }
}