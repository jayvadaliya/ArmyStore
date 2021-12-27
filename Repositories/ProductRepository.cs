using System.Data;
using ArmyStore.DataModels;
using ArmyStore.Entities;
using ArmyStore.Helper;
using ArmyStore.Interfaces;
using Dapper;

namespace ArmyStore.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly IDapperContext _dapperContext;
        private readonly IMapper<Product, ProductModel> _mapper;
        private readonly IMapper<ProductMetadata, ProductMetadataModel> _productMetadataMapper;
        private readonly IRepository<ProductMetadata> _metadataRepository;

        public ProductRepository(
            IDapperContext dapperContext,
            IMapper<Product, ProductModel> mapper,
            IMapper<ProductMetadata, ProductMetadataModel> productMetadataMapper,
            IRepository<ProductMetadata> metadataRepository)
        {
            _dapperContext = dapperContext;
            _mapper = mapper;
            _productMetadataMapper = productMetadataMapper;
            _metadataRepository = metadataRepository;
        }

        public async Task Create(Product product, bool useTransaction = false)
        {
            var productModel = _mapper.MapToDataModel(product);
            var productId = await InsertProduct(productModel, useTransaction);
            if (product.ProductMetadata != null)
            {
                product.ProductMetadata.LinkProductId(productId);
                await _metadataRepository.Create(product.ProductMetadata);
            }
        }

        public async Task<IEnumerable<Product>> GetAll(bool useTransaction = false)
        {
            using (IDbConnection conn = _dapperContext.Connection)
            {
                var entities = new List<Product>();
                await conn.QueryAsync<ProductModel, ProductMetadataModel, Product>(
                    SqlQueries.GET_PRODUCT_DETAIL,
                    (productModel, ProductMetadataModel) =>
                    {
                        var entity = _mapper.MapToDomain(productModel);
                        entity.LinkMetadata(_productMetadataMapper.MapToDomain(ProductMetadataModel));
                        entities.Add(entity);
                        return entity;
                    },
                    transaction: useTransaction ? _dapperContext.GetTransaction() : null);

                return entities;
            }
        }

        public async Task<Product> GetById(long id, bool useTransaction = false)
        {
            string where = " WHERE product.id = @id";
            var param = new { Id = new DbString { Value = id.ToString() } };

            using (IDbConnection conn = _dapperContext.Connection)
            {
                var result = await conn.QueryFirstOrDefaultAsync<ProductModel>(
                    string.Concat(SqlQueries.GET_PRODUCT_DETAIL, where),
                    param,
                    transaction: useTransaction ? _dapperContext.GetTransaction() : null);
                return _mapper.MapToDomain(result);
            }
        }

        public async Task Delete(long id, bool useTransaction = false)
        {
            var param = new { Id = new DbString { Value = id.ToString() } };

            await _metadataRepository.Delete(id, false);
            using (IDbConnection conn = _dapperContext.Connection)
            {
                await conn.ExecuteAsync(
                    SqlQueries.DELETE_PRODUCT,
                    param,
                    transaction: useTransaction ? _dapperContext.GetTransaction() : null);
                conn.Close();
            }
        }

        private async Task<long> InsertProduct(ProductModel model, bool useTransaction)
        {
            var fields = new List<string>
            {
                "name",
                "price",
                "image_url",
                "status",
                "updated_on"
            };

            var insertQueryValues = SqlHelper.GetQueryValues(fields);
            string insertQuery = $"INSERT INTO product {insertQueryValues}; SELECT CONVERT(LAST_INSERT_ID(), SIGNED INTEGER) AS ID;";
            using (IDbConnection conn = _dapperContext.Connection)
            {
                return await conn.ExecuteScalarAsync<long>(
                    insertQuery,
                    model,
                    transaction: useTransaction ? _dapperContext.GetTransaction() : null);
            }
        }
    }
}