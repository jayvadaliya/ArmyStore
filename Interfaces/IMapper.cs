namespace ArmyStore.Interfaces
{
    public interface IMapper<TDomain, TDatamodel>
    {
        TDatamodel MapToDataModel(TDomain source);
        TDomain MapToDomain(TDatamodel source);
    }
}