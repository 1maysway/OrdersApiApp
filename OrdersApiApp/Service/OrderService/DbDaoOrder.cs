using Microsoft.EntityFrameworkCore;
using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.OrderService
{
    public class DbDaoOrder: IDaoOrder
    {
        private readonly ApplicationDbContext context;

        public DbDaoOrder(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Order> AddOrder(Order order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            Console.WriteLine("ORDERS -- DELETE");
            Console.WriteLine("Id => "+id);
            Order order = await context.Orders.FirstOrDefaultAsync((ord) => ord.Id==id);
            if(order != null)
            {
                Console.WriteLine("Order is not null.");
                context.Remove(order);
                await context.SaveChangesAsync();
            }
            return order != null;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await context.Orders.FirstOrDefaultAsync((ord) => ord.Id == id);
        }


        public async Task<Order> UpdateOrder(Order updatedOrder)
        {
            context.Orders.Update(updatedOrder);
            await context.SaveChangesAsync();
            return updatedOrder;
        }
    }
}
