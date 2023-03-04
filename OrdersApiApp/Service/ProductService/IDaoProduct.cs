using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.ProductService
{
    public interface IDaoProduct
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product updatedProduct);
        Task<bool> DeleteProduct(int id);
    }
}
