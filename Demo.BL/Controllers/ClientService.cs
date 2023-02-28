using Demo.Data.DB.SqlServer;
using Demo.Data.Repositories.Implementations;
using Demo.Data.Repositories.Interfaces;
using Demo.Models.Entities;

namespace Demo.BL.Controllers
{
    public class ClientService
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Order> _orderRepository;
        public ClientService()
        {

        }
        public ClientService(IRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public List<Client> GetClients()
        {
            return _clientRepository.GetAll().ToList();
        }

        public void AddClient(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (string.IsNullOrWhiteSpace(client.FirstName))
            {
                throw new ArgumentException("First name cannot be empty.",
                    nameof(client.FirstName));
            }

            if (string.IsNullOrWhiteSpace(client.LastName))
            {
                throw new ArgumentException("Last name cannot be empty.",
                    nameof(client.LastName));
            }

            if (string.IsNullOrWhiteSpace(client.PhoneNum))
            {
                throw new ArgumentException("Phone number cannot be empty.",
                    nameof(client.PhoneNum));
            }

            _clientRepository.Add(client);
            _clientRepository.SaveChanges();
        }

        public void EditClient(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (client.Id == 0)
            {
                throw new ArgumentException("Client ID cannot be zero.",
                    nameof(client.Id));
            }

            if (string.IsNullOrWhiteSpace(client.FirstName))
            {
                throw new ArgumentException("First name cannot be empty.",
                    nameof(client.FirstName));
            }

            if (string.IsNullOrWhiteSpace(client.LastName))
            {
                throw new ArgumentException("Second name cannot be empty.",
                    nameof(client.LastName));
            }

            if (string.IsNullOrWhiteSpace(client.PhoneNum))
            {
                throw new ArgumentException("Phone number cannot be empty.",
                    nameof(client.PhoneNum));
            }

            var existingClient = _clientRepository.GetById(client.Id);
            if (existingClient == null)
            {
                throw new ArgumentException($"Client with ID {client.Id} not found.",
                    nameof(client.Id));
            }

            existingClient.FirstName = client.FirstName;
            existingClient.LastName = client.LastName;
            existingClient.PhoneNum = client.PhoneNum;
            existingClient.OrderAmount = client.OrderAmount;
            existingClient.DateAdd = client.DateAdd;

            _clientRepository.Edit(existingClient);
            _clientRepository.SaveChanges();
        }

        public void DeleteClient(uint clientId)
        {
            if (clientId == 0)
            {
                throw new ArgumentException("Client ID cannot be zero.",
                    nameof(clientId));
            }

            var existingClient = _clientRepository.GetById(clientId);
            if (existingClient == null)
            {
                throw new ArgumentException($"Client with ID {clientId}" +
                    $"not found.", nameof(clientId));
            }

            _clientRepository.Delete(existingClient);
            _clientRepository.SaveChanges();
        }

        public List<Order> GetClientOrders(uint clientId)
        {
            if (clientId == 0)
            {
                throw new ArgumentException("Client ID cannot be zero.",
                    nameof(clientId));
            }

            var client = _clientRepository.GetById(clientId);
            if (client == null)
            {
                throw new ArgumentException($"Client with ID {clientId} not found.",
                    nameof(clientId));
            }

            List<Order> orders = _orderRepository.GetAll()
                .Where(o => o.ClientId == clientId).ToList();
            return orders;
        }
        public Client GetClientById(uint clientId)
        {
            if (clientId == 0)
            {
                throw new ArgumentException("Client ID cannot be zero.",
                    nameof(clientId));
            }

            var existingClient = _clientRepository.GetById(clientId);
            if (existingClient == null)
            {
                throw new ArgumentException($"Client with ID {clientId} not found.",
                    nameof(clientId));
            }

            return existingClient;
        }
    }
}
