using Demo.BL.Controllers;
using Demo.Data.DB.SqlServer;
using Demo.Data.Repositories.Implementations;
using Demo.Models.Entities;

namespace Demo.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AppDbContext db = new AppDbContext())
            {
                var clientService = new ClientService(new Repository<Client>(db));
                var orderService = new OrderService(new Repository<Order>(db));

                while (true)
                {

                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine("1. Add new client");
                    Console.WriteLine("2. Add new order");
                    Console.WriteLine("3. List all clients");
                    Console.WriteLine("4. List all orders");
                    Console.WriteLine("5. Exit");

                    var input = ConsoleReader<int>.Read("option");

                    switch (input)
                    {
                        case 1:
                                Console.WriteLine("Please enter the following client details:");

                            var firstName = ConsoleReader<string>.Read("first name");
                            var lastName = ConsoleReader<string>.Read("last name");
                            var phoneNum = ConsoleReader<string>.Read("phone number");

                            var client = new Client
                            {
                                FirstName = firstName,
                                LastName = lastName,
                                PhoneNum = phoneNum
                            };

                            clientService.AddClient(client);

                            Console.WriteLine("Client added successfully.");
                            Console.WriteLine();
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

                            Console.WriteLine();
                            break;

                        case 4:
                            // List all orders functionality here
                            break;

                        case 5:
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