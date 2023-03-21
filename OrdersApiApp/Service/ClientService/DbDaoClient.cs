using Microsoft.EntityFrameworkCore;
using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;



namespace OrdersApiApp.Service.ClientService
{
    public class DbDaoClient : IDaoClient
    {
        private readonly ApplicationDbContext context;

        public DbDaoClient(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Client> AddClient(Client client)
        {
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
            return client;
        }

        public async Task<bool> DeleteClient(int id)
        {
            Client client = await context.Clients.FirstOrDefaultAsync(client => client.Id==id);
            if(client != null)
            {
                context.Remove(client);
            }
            return client != null;
        }

        public async Task<Client> GetClientByUsername(string username)
        {
            return await context.Clients.FirstOrDefaultAsync(client => client.Name == username);
        }

        public async Task<List<Client>> GetAllClients()
        {
            return await context.Clients.ToListAsync();
        }

        public async Task<Client> GetClientById(int id)
        {
            return await context.Clients.FirstOrDefaultAsync(client => client.Id==id);
        }

        public async Task<Client> UpdateClient(Client updatedClient)
        {
            context.Clients.Update(updatedClient);
            await context.SaveChangesAsync();
            return updatedClient;
        }
    }
}
