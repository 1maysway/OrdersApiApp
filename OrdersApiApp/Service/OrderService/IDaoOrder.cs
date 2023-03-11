using OrdersApiApp.Model.Entity;
using OrdersApiApp.Service.OrderService.Entity;

namespace OrdersApiApp.Service.OrderService
{
    public interface IDaoOrder
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<Order> AddOrder(Order order);
        Task<Order> UpdateOrder(Order updatedOrder);
        Task<bool> DeleteOrder(int id);
        Task<OrderInfo> GetOrderInfo(int id);
    }
}
