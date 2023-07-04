using CustomerOrderViewer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOrderViewer.Repository
{
    class CustomerCommand
    {
        private string _connectionString;

        public CustomerCommand(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IList<CustomerModel> GetList()
        {
            List<CustomerModel> list = new List<CustomerModel>();
            var sql = "Customer_GetList";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                list = connection.Query<CustomerModel>(sql).ToList();
            }

            return list;
        }
    }
}
