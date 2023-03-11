using Microsoft.EntityFrameworkCore;
using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.OrderProductsService
{
    public class DbDaoOrderProducts: IDaoOrderProducts
    {
        private readonly ApplicationDbContext context;

        public DbDaoOrderProducts(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<OrderProducts> AddOrderProducts(OrderProducts orderProducts)
        {
            await context.OrderProducts.AddAsync(orderProducts);
            await context.SaveChangesAsync();
            return orderProducts;
        }

        public async Task<bool> DeleteOrderProducts(int id)
        {
            OrderProducts orderProducts = await context.OrderProducts.FirstOrDefaultAsync(orderProd => orderProd.Id==id);
            if(orderProducts != null)
            {
                context.OrderProducts.Remove(orderProducts);
            }
            return orderProducts != null;
        }

        public async Task<List<OrderProducts>> GetAllOrderProducts()
        {
            return await context.OrderProducts.ToListAsync();
        }

        public async Task<OrderProducts> GetOrderProductsById(int id)
        {
            return await context.OrderProducts.FirstOrDefaultAsync(orderProd => orderProd.Id==id);
        }

        public async Task<OrderProducts> UpdateOrderProducts(OrderProducts updatedOrderProduct)
        {
            context.OrderProducts.Update(updatedOrderProduct);
            await context.SaveChangesAsync();
            return updatedOrderProduct;
        }
    }
}
