using System.Data;
using ArmyStore.DataModels;
using ArmyStore.Entities;
using ArmyStore.Helper;
using ArmyStore.Interfaces;
using Dapper;

namespace ArmyStore.Repositories
{
    public class ProductMetadataRepository : IRepository<ProductMetadata>
    {
        private readonly IDapperContext _dapperContext;
        private readonly IMapper<ProductMetadata, ProductMetadataModel> _mapper;

        public ProductMetadataRepository(
            IDapperContext dapperContext,
            IMapper<ProductMetadata, ProductMetadataModel> mapper)
        {
            _dapperContext = dapperContext;
            _mapper = mapper;
        }

        public async Task Create(ProductMetadata productMetadata, bool useTransaction = false)
        {
            var model = _mapper.MapToDataModel(productMetadata);
            await InsertProductMetadata(model, useTransaction);
        }

        public Task<ProductMetadata> GetById(long id, bool useTransaction = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductMetadata>> GetAll(string searchTerm, bool useTransaction = false)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(long id, bool useTransaction = false)
        {
            var param = new { Id = new DbString { Value = id.ToString() } };

            using (IDbConnection conn = _dapperContext.Connection)
            {
                await conn.ExecuteAsync(
                    SqlQueries.DELETE_PRODUCT_METADATA,
                    param,
                    transaction: useTransaction ? _dapperContext.GetTransaction() : null);
                conn.Close();
            }
        }

        private async Task<long> InsertProductMetadata(ProductMetadataModel model, bool useTransaction)
        {
            var fields = new List<string>
            {
                "id",
                "description",
                "specifications"
            };

            var insertQueryValues = SqlHelper.GetQueryValues(fields);
            string insertQuery = $"INSERT INTO product_metadata {insertQueryValues}";
            using (IDbConnection conn = _dapperContext.Connection)
            {
                return await conn.ExecuteAsync(
                    insertQuery,
                    model,
                    transaction: useTransaction ? _dapperContext.GetTransaction() : null);
            }
        }
    }
}