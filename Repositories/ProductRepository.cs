using System.Data;
using ArmyStore.DataModels;
using ArmyStore.Entities;
using ArmyStore.Interfaces;
using Dapper;

namespace ArmyStore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDapperContext _dapperContext;
        private readonly IMapper<Product, ProductModel> _mapper;

        public ProductRepository(IDapperContext dapperContext, IMapper<Product, ProductModel> mapper)
        {
            _dapperContext = dapperContext;
            _mapper = mapper;
        }

        public Task<Product> Create(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetList()
        {
            using (IDbConnection conn = _dapperContext.Connection)
            {
                var result = await conn.QueryAsync<ProductModel>(SqlQueries.GET_PRODUCT_DETAIL);
                return result.Select(x => _mapper.MapToDomain(x));
            }
        }

        public async Task<Product> GetProduct(int id)
        {
            var param = new { Id = new DbString { Value = id.ToString() } };

            using (IDbConnection conn = _dapperContext.Connection)
            {
                var result = await conn.QueryFirstOrDefaultAsync<ProductModel>(SqlQueries.GET_PRODUCT_DETAIL, param);
                return _mapper.MapToDomain(result);
            } 
        }
    }
}