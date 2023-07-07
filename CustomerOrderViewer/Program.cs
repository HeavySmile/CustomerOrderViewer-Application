using CustomerOrderViewer.Models;
using CustomerOrderViewer.Repository;
using System.Data.SqlClient;

namespace CustomOrderViewer
{
    class Program
    {
        private static string _connectionString = @"Data Source=localhost;Initial Catalog=CustomerOrderViewer;Integrated Security=True";
        private static readonly CustomerOrderDetailCommand _customerOrderDetailCommand = new CustomerOrderDetailCommand(_connectionString);
        private static readonly CustomerCommand _customerCommand = new CustomerCommand(_connectionString);
        private static readonly ItemCommand _itemCommand = new ItemCommand(_connectionString);

        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("What is your username?");
                var uid = Console.ReadLine();
                var continueUsage = true;

                do
                {
                    Console.WriteLine("1. Show All\n2. Upsert Customer Order\n3. Delete Customer Order\n4. Exit");
                    int option; 
                    
                    if (!Int32.TryParse(Console.ReadLine(), out option))
                    {
                        Console.WriteLine("Incorrect input!");
                        continue;
                    }

                    switch(option) 
                    {
                        case 1:
                            ShowAll();
                            break;
                        case 2:
                            UpsertCustomerOrder(uid);
                            break;
                        case 3:
                            DeleteCustomerOrder(uid);
                            break;
                        case 4:
                            continueUsage = false;
                            break;
                        default:
                            Console.WriteLine("Incorrect input!");
                            break;
                    }
                } 
                while (continueUsage);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Something went wrong: {ex.Message}");
            }
        }

        private static void DeleteCustomerOrder(string uid)
        {
            Console.WriteLine("Enter Customer Order Id: ");
            int customerOrderId;

            if (!Int32.TryParse(Console.ReadLine(), out customerOrderId)) 
            {
                Console.WriteLine("Incorrect input!");
                return;
            }

            _customerOrderDetailCommand.Delete(customerOrderId, uid);
        }

        private static void UpsertCustomerOrder(string uid)
        {
            int customerOrderId;
            int customerId;
            int itemId;

            Console.WriteLine("Note: for updating insert existing Customer Order Id, for new entry enter -1");

            Console.WriteLine("Enter Customer Order Id: ");
            if (!Int32.TryParse(Console.ReadLine(), out customerOrderId))
            {
                Console.WriteLine("Incorrect input!");
                return;
            }

            Console.WriteLine("Enter Customer Id: ");
            if (!Int32.TryParse(Console.ReadLine(), out customerId))
            {
                Console.WriteLine("Incorrect input!");
                return;
            }

            Console.WriteLine("Enter Item Id: ");
            if (!Int32.TryParse(Console.ReadLine(), out itemId))
            {
                Console.WriteLine("Incorrect input!");
                return;
            }

            _customerOrderDetailCommand.Upsert(customerOrderId, customerId, itemId, uid);
        }

        private static void ShowAll()
        {
            Console.WriteLine("\nAll Customer Order:\n");
            DisplayCustomerOrders();

            Console.WriteLine("\nAll Customers:\n");
            DisplayCustomers();

            Console.WriteLine("\nAll Items:\n");
            DisplayItems();

            Console.WriteLine();
        }

        private static void DisplayItems()
        {
            IList<ItemModel> items = _itemCommand.GetList().ToList();

            if (items.Any()) 
            {
                foreach (var item in items) 
                {
                    Console.WriteLine("{0}: Description: {1}, Price: {2}",
                        item.ItemId,
                        item.Description,
                        item.Price
                    );
                }
            }
        }

        private static void DisplayCustomers()
        {
            IList<CustomerModel> customers = _customerCommand.GetList();

            if (customers.Any())
            {
                foreach (var customer in customers)
                {
                    Console.WriteLine("{0}: FirstName: {1}, MiddleName: {2}, LastName: {3}, Age: {4}",
                        customer.CustomerId,
                        customer.FirstName,
                        customer.MiddleName ?? "N/A", 
                        customer.LastName,
                        customer.Age
                    );
                }
            }
        }

        private static void DisplayCustomerOrders()
        {

            IList<CustomerOrderDetailModel> customerOrders = _customerOrderDetailCommand.GetList();

            if (customerOrders.Any())
            {
                foreach (var customerOrder in customerOrders)
                {
                    Console.WriteLine("{0}: Fullname: {1} {2} (Id: {3} - purchased {4} for {5} (Id: {6})",
                        customerOrder.CustomerOrderId,
                        customerOrder.FirstName,
                        customerOrder.LastName,
                        customerOrder.CustomerId,
                        customerOrder.Description,
                        customerOrder.Price,
                        customerOrder.ItemId
                    );
                }
            }
        }
    }
}
