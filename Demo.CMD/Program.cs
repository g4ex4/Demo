using Demo.BL.Controllers;
using Demo.Data.DB.SqlServer;
using Demo.Data.Repositories.Implementations;
using Demo.Models.Entities;
using System;

namespace Demo.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext())
            {
                var clientService = new ClientService(new Repository<Client>(db));
                var orderService = new OrderService(new Repository<Client>(db),new Repository<Order>(db));

                while (true)
                {

                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine("1. Add new client");
                    Console.WriteLine("2. Add new order");
                    Console.WriteLine("3. List all clients");
                    Console.WriteLine("4. List all orders");
                    Console.WriteLine("5. Edit Client");
                    Console.WriteLine("6. Delete Client");
                    Console.WriteLine("7. Edit order");
                    Console.WriteLine("8. Delete Order");
                    Console.WriteLine("9. Exit");

                    var input = ConsoleReader<int>.Read("option");

                    switch (input)
                    {
                        case 1:
                            Console.WriteLine("Please enter the following client details:");

                            var firstName = ConsoleReader<string>.Read("first name");
                            var lastName = ConsoleReader<string>.Read("last name");
                            var phoneNum = ConsoleReader<string>.Read("phone number");

                            Client client = new Client
                            {
                                FirstName = firstName,
                                LastName = lastName,
                                PhoneNum = phoneNum
                            };

                            clientService.AddClient(client);

                            Console.WriteLine("Client added successfully.");
                            Console.WriteLine("Press any key");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        case 2:
                            uint clientId = ConsoleReader<uint>.Read("client ID");

                            // Получаем информацию о заказе
                            string description = ConsoleReader<string>.Read("product name");
                            float price = ConsoleReader<float>.Read("price");

                            // Создаем новый заказ
                            Order newOrder = new Order()
                            {
                                Description = description,
                                Price = price,
                                ClientId = clientId
                            };

                            // Добавляем заказ в список заказов
                            orderService.AddOrder(newOrder);

                            Console.WriteLine("Order added successfully.");
                            Console.WriteLine("Press any key");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        case 3:
                            Console.WriteLine("List of all clients:");

                            var clients = clientService.GetClients();

                            if (clients.Any())
                            {
                                foreach (var c in clients)
                                {
                                    Console.WriteLine($"ID: {c.Id} - {c.FirstName} {c.LastName} ({c.PhoneNum})");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No clients found.");
                            }
                            Console.WriteLine("Press any key");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        case 4:
                            var orders = orderService.GetAllOrders();
                            if (orders.Count == 0)
                            {
                                Console.WriteLine("There are no orders.");
                                return;
                            }
                            foreach (var order in orders)
                            {
                                Console.WriteLine($"Order Id: {order.Id}," +
                                    $"Client Id: {order.ClientId}, Client FullName" +
                                    $"{order.Client.FullName} Order Description: {order.Description}" +
                                    $"Price: {order.Price}");
                            }
                            Console.WriteLine("Press any key");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 5:
                            clientId = ConsoleReader<uint>.Read("client ID");

                            firstName = ConsoleReader<string>.Read("first name");
                            lastName = ConsoleReader<string>.Read("last name");
                            phoneNum = ConsoleReader<string>.Read("phone number");
                            client = clientService.GetClientById(clientId);
                            client.FirstName = firstName;
                            client.LastName = lastName;
                            client.PhoneNum = phoneNum;
                            Console.WriteLine("Press any key");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 6:
                            clientId = ConsoleReader<uint>.Read("client Id");
                            client = clientService.GetClientById(clientId);
                            clientService.DeleteClient(clientId);
                            orderService.DeleteOrder(clientId);
                            Console.WriteLine("Client deleted/n Press any key");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case 7:
                            var orderId = ConsoleReader<uint>.Read("Order ID");
                            description = ConsoleReader<string>.Read("product name");
                            price = ConsoleReader<float>.Read("price");
                            orderService.GetClientOrders(orderId);
                            newOrder = new Order()
                            {
                                Description = description,
                                Price = price
                            };

                            Console.WriteLine("Order edited/n Press any key");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        case 8:
                            orderId = ConsoleReader<uint>.Read("Order ID");
                            orderService.GetClientOrders(orderId);
                            orderService.DeleteOrder(orderId);
                            Console.WriteLine("Order deleted/n Press any key");
                            Console.ReadKey();
                            Console.Clear();
                            break;


                        case 9:
                            Console.WriteLine("Exiting program...");
                            return;
                        default:
                            Console.WriteLine("Invalid option selected. Please try again.");
                            Console.WriteLine();
                            break;
                    }
                }
            }
        }
    }
}