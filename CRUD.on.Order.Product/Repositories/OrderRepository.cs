using CRUD.on.Order.Product.Context;
using CRUD.on.Order.Product.DTO;
using CRUD.on.Order.Product.Model;
using CRUD.on.Order.Product.Repositories.Interfaces;
using CRUD.on.Order.Product.Model;
using Dapper;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace CRUDonOrderProduct.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _Context;
        public OrderRepository(DapperContext context)
        {
            _Context = context;
        }

        public async Task<int> CreateOrder(CreateOrderDto ord)
        {
            //custName, address, ordDate
            int result = 0;
            var qry = " INSERT INTO tblOrder (custName, address, ordDate, grandTotal) VALUES " +
                      " (@custName, @address, @ordDate, @grandTotal) " +
                      " SELECT CAST(SCOPE_IDENTITY() as int) ";

            var obj = new DynamicParameters();
            obj.Add("custName", ord.custName);
            obj.Add("address", ord.address);
            obj.Add("ordDate", ord.ordDate);
            obj.Add("grandTotal", 0);

            using (var conn = _Context.CreateConnection())
            {
                result = await conn.QuerySingleAsync<int>(qry, obj);

                if(result != 0)
                {
                    var result1 = await AddProduct(ord.prod, result);
                    await conn.ExecuteAsync("UPDATE tblOrder SET grandTotal = @grandTotal WHERE oId = @oId", new { grandTotal = result1, oId = result});
                }

                return result;
            }
        }

        private async Task<double> AddProduct(List<tblProduct> prod, int oId)
        {
            int result = 0;
            double grandTotal = 0;
            using(var conn = _Context.CreateConnection())
            {
                if(prod.Count > 0)
                {
                    foreach(tblProduct product in prod)
                    {
                        product.oId = oId;
                        product.totalAmount = product.prodPrice * product.prodQty;
                        var qry = " insert into tblProduct" +
                                  " (prodName,prodPrice,prodQty,totalAmount,oId) " +
                                  " values (@prodName,@prodPrice,@prodQty,@totalAmount,@oId) ";
                        var result1 = await conn.ExecuteAsync(qry, product);

                        grandTotal += product.totalAmount;
                    }
                }
                return grandTotal;
            }
        }

        public Task<int> DeleteOrder(int oId)
        {
            throw new NotImplementedException();
        }

        public async Task<tblOrder> GetOrder(int oId)
        {
            var qry = " SELECT * FROM tblOrder WHERE oId = @oId " +
                      " SELECT * FROM tblProduct WHERE oId = @oId ";
            using (var connection = _Context.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(qry, new { oId }))
            {
                var ord = await multi.ReadSingleOrDefaultAsync<tblOrder>();
                if (ord != null)
                    ord.prod = (await multi.ReadAsync<tblProduct>()).ToList();
                return ord;
            }
        }

        public async Task<List<tblOrder>> GetOrders()
        {
            List<tblOrder> orders = new List<tblOrder>();
            var qry1 = "SELECT * FROM tblOrder";
            using(var connection = _Context.CreateConnection())
            {
                var res1 = await connection.QueryAsync<tblOrder>(qry1);
                orders = res1.ToList();

                foreach(var order in orders)
                {
                    var qry2 = "SELECT * FROM tblProduct WHERE ordId = @ordId";
                    var res2 = await connection.QueryAsync<tblProduct>(qry2);

                    order.prod = res2.ToList();
                }
                return orders;
            }
        }

        public async Task<int> UpdateOrder(UpdateOrderDto ord)
        {
            //oId, custName,address ,ordDate 

            int result = 0;
            var qry = " UPDATE tblOrder SET custName = @custName, address = @address " +
                      " ordDate = @ordDate WHERE oId = @oId ";
            using (var conn = _Context.CreateConnection())
            {
                result = await conn.ExecuteAsync(qry, ord);
                if (result != 0)
                {
                    var qry2 = "DELETE FROM tblProduct WHERE oId = @oId";
                    result = await conn.ExecuteAsync(qry2, new { oId = ord.oId });
                    var result1 = await AddProduct(ord.prod, ord.oId);
                }

                return result;
            }
        }
    }
}
