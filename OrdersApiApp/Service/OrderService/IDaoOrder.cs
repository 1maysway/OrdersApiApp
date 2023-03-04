using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.OrderService
{
    public interface IDaoOrder
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<Order> AddOrder(Order order);
        Task<Order> UpdateOrder(Order updatedOrder);
        Task<bool> DeleteOrder(int id);

        // Task<> GetOrderInfo(int id);
        // Task<> GetBill(int id);
        // Как типизировать запрос на чек и информацию о заказе?
    }
}
