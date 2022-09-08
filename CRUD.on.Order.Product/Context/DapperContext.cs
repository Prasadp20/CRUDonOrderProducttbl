using System.Data;
using Microsoft.Data.SqlClient;

namespace CRUD.on.Order.Product.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            this.connectionString = _configuration.GetConnectionString("SqlConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(connectionString);
    }
}
