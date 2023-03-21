using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.ClientService
{
    public interface IDaoClient
    {
        Task<List<Client>> GetAllClients();
        Task<Client> GetClientById(int id);
        Task<Client> AddClient(Client client);
        Task<Client> UpdateClient(Client updatedClient);
        Task<bool> DeleteClient(int id);
        Task<Client> GetClientByUsername(string username);
    }
}
