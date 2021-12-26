namespace ArmyStore.Interfaces
{
    public interface IRepository<TDomain>
    {
        Task Create(TDomain product, bool useTransaction = false);

        Task<TDomain> GetById(int id, bool useTransaction = false);

        Task<IEnumerable<TDomain>> GetAll(bool useTransaction = false);
    }
}