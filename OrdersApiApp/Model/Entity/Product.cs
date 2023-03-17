namespace OrdersApiApp.Model.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Article { get; set; }
        public float Price { get; set; }
        public int QuantityInStock { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Title} - {Price}";
        }
    }
}
