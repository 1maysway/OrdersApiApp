using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.OrderService.Entity
{
    public class OrderInfo
    {
        public ProductForOrderInfo[]? Products { get; set; }
        public float Total { get; set; }

        public OrderInfo(ProductForOrderInfo[] productsToReciept)
        {
            Products = productsToReciept;
            Total = Products.Sum(p => p.Total);
        }
    }
}
