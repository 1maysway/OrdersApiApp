using Microsoft.EntityFrameworkCore;
using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.ProductService
{
    public class DbDaoProduct : IDaoProduct
    {
        private readonly ApplicationDbContext context;

        public DbDaoProduct(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Product> AddProduct(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            Product product = await context.Products.FirstOrDefaultAsync(prod => prod.Id==id);
            if(product != null)
            {
                context.Remove(product);
            }
            return product != null;
        }

        public Task<List<Product>> GetAllProducts()
        {
            return context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await context.Products.FirstOrDefaultAsync(prod => prod.Id ==id);
        }

        public async Task<Product> UpdateProduct(Product updatedProduct)
        {
            context.Products.Update(updatedProduct);
            await context.SaveChangesAsync();
            return updatedProduct;
        }
    }
}
