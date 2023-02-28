using Demo.Data.Repositories.Interfaces;
using Demo.Models.Entities;

namespace Demo.BL.Controllers
{
    public class OrderService
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Order> _orderRepository;
        public OrderService()
        {

        }
        public OrderService(IRepository<Client> clientRepository, IRepository<Order> orderRepository)
        {
            _clientRepository = clientRepository;
            _orderRepository = orderRepository;
        }

        public void AddOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null");
            }

            if (order.ClientId == 0)
            {
                throw new ArgumentException("Order must have a valid client ID");
            }

            var client = _clientRepository.GetById(order.ClientId);

            if (client == null)
            {
                throw new ArgumentException($"Client with ID {order.ClientId} does not exist");
            }

            _orderRepository.Add(order);
        }

        public void EditOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null");
            }

            if (order.ClientId == 0)
            {
                throw new ArgumentException("Order must have a valid client ID");
            }

            var client = _clientRepository.GetById(order.ClientId);

            if (client == null)
            {
                throw new ArgumentException($"Client with ID {order.ClientId} does not exist");
            }

            _orderRepository.Edit(order);
        }

        public void DeleteOrder(uint orderId)
        {
            var order = _orderRepository.GetById(orderId);

            if (order == null)
            {
                throw new ArgumentException($"Order with ID {orderId} does not exist");
            }

            _orderRepository.Delete(order);
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        public List<Order> GetClientOrders(uint clientId)
        {
            var clientExists = _clientRepository.GetAll().Any(client => client.Id == clientId);

            if (!clientExists)
            {
                throw new ArgumentException($"Client with ID {clientId} does not exist");
            }

            return _orderRepository.GetAll().Where(order => order.ClientId == clientId).ToList();
        }
    }
}
