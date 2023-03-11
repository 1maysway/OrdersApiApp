using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.OrderService.Entity
{
    public class ProductForOrderInfo
    {
        public Product? Product { get; set; }
        public int Count { get; set; }
        public float Total { get; set; }

        public ProductForOrderInfo(Product product,int count)
        {
            Product = product;
            Count = count;
            Total = product.Price * count;
        }
    }
}
