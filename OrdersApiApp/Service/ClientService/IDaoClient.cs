using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.ClientService
{
    // DAO (data-access-object) интерфейс для работы с клиентом
    public interface IDaoClient
    {
        // вариант синхронный
        //List<Client> GetAllClients();
        //Client GetClientById(int id);
        //Client AddClient(Client client);
        //Client UpdateClient(Client client);
        //bool DeleteClient(int id);

        Task<List<Client>> GetAllClients();
        Task<Client> GetClientById(int id);
        Task<Client> AddClient(Client client);
        Task<Client> UpdateClient(Client updatedClient);
        Task<bool> DeleteClient(int id);
    }
}
