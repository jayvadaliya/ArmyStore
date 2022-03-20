namespace ArmyStore.Interfaces
{
    public interface IRepository<TDomain>
    {
        Task Create(TDomain product, bool useTransaction = false);

        Task<TDomain> GetById(long id, bool useTransaction = false);

        Task<IEnumerable<TDomain>> GetAll(string searchTerm, bool useTransaction = false);

        Task Delete(long id, bool useTransaction = false);
    }
}