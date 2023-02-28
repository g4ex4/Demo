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
                var orderService = new OrderService(new Repository<Client>(db),new Repository<Order>(db));

                while (true)
                {

                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine("1. ClientsTable");
                    Console.WriteLine("2. OrdersTable");
                    Console.WriteLine("3. Exit");

                    var input = ConsoleReader<int>.Read("option");

                    switch (input)
                    {
                        case 1:
                           
                            break;

                        case 2:
                           while (true)
                            {

                            }
                            break;

                        case 3:
                            Console.WriteLine("Exiting program...");
                            return;

                        default:
                            Console.WriteLine("Invalid option selected. Please try again.");
                            Console.WriteLine();
                            Console.Clear();
                            break;
                    }
                }
            }
        }
    }
}