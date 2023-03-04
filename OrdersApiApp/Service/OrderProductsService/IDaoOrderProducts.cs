using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.OrderProductsService
{
    public interface IDaoOrderProducts
    {
        Task<List<OrderProducts>> GetAllOrderProducts();
        Task<OrderProducts> GetOrderProductsById(int id);
        Task<OrderProducts> AddOrderProducts(OrderProducts OrderProduct);
        Task<OrderProducts> UpdateOrderProducts(OrderProducts updatedOrderProduct);
        Task<bool> DeleteOrderProducts(int id);
    }
}
