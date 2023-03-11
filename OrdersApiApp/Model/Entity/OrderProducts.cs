namespace OrdersApiApp.Model.Entity
{
    public class OrderProducts
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return Product.ToString();
        }
    }
}
