using CustomerOrderViewer.Models;
using CustomerOrderViewer.Repository;
using System.Data.SqlClient;

namespace CustomOrderViewer
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                CustomerOrderDetailCommand customerOrderDetailCommand = new CustomerOrderDetailCommand("Data Source=localhost;Initial Catalog=CustomerOrderViewer;Integrated Security=True");
                IList<CustomerOrderDetailModel> customerOrderDetailModels = customerOrderDetailCommand.GetList();

                if (customerOrderDetailModels.Any())
                {
                    foreach (var model in customerOrderDetailModels) 
                    { 
                        Console.WriteLine("{0}: Fullname: {1} {2} (Id: {3}) - purchased {4} for {5} (Id: {6})",
                            model.CustomerOrderId, 
                            model.FirstName,
                            model.LastName,
                            model.CustomerId,
                            model.Description,
                            model.Price,
                            model.ItemId
                            );
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Something went wrong {ex.Message}");
            }
        }
    }
}
