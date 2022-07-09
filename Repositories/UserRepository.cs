using System.Data;
using ArmyStore.Entities;
using ArmyStore.Helper;
using ArmyStore.Interfaces;
using Dapper;

namespace ArmyStore.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly IDapperContext _dapperContext;

        public UserRepository(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task Create(User user, bool useTransaction = false)
        {
            var fields = new List<string>
            {
                "name",
                "password_hash",
                "password_salt",
                "updated_on"
            };

            var insertQueryValues = SqlHelper.GetQueryValues(fields);
            string insertQuery = $"INSERT INTO user {insertQueryValues}; SELECT CONVERT(LAST_INSERT_ID(), SIGNED INTEGER) AS ID;";
            using (IDbConnection conn = _dapperContext.Connection)
            {
                await conn.ExecuteScalarAsync<long>(
                    insertQuery,
                    user,
                    transaction: useTransaction ? _dapperContext.GetTransaction() : null);
            }
        }

        public Task Delete(long id, bool useTransaction = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAll(string searchTerm, bool useTransaction = false)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(long id, bool useTransaction = false)
        {
            string where = " WHERE user.id = @Id";
            var param = new { Id = new DbString { Value = id.ToString() } };

            using (IDbConnection conn = _dapperContext.Connection)
            {
                var result = await conn.QueryAsync<User>(
                    string.Concat(SqlQueries.GET_USER_DETAIL, where),
                    param,
                    transaction: useTransaction ? _dapperContext.GetTransaction() : null);

                return result.FirstOrDefault();
            }
        }

        public async Task<User> GetByUserName(string userName, bool useTransaction = false)
        {
            string where = " WHERE user.name = @username";
            var param = new { username = new DbString { Value = userName } };

            using (IDbConnection conn = _dapperContext.Connection)
            {
                var result = await conn.QueryAsync<User>(
                    string.Concat(SqlQueries.GET_PRODUCT_DETAIL, where),
                    param,
                    transaction: useTransaction ? _dapperContext.GetTransaction() : null);

                return result.FirstOrDefault();
            }
        }
    }
}