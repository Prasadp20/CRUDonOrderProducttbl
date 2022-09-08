
using CRUD.on.Order.Product.DTO;
using CRUD.on.Order.Product.Model;

namespace CRUD.on.Order.Product.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public Task<List<tblOrder>> GetOrders();
        public Task<tblOrder> GetOrder(int oId);
        public Task<int> CreateOrder(CreateOrderDto ord);
        public Task<int> UpdateOrder(UpdateOrderDto ord);
        public Task<int> DeleteOrder(int oId);
    }
}
