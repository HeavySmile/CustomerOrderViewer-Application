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
    class CustomerOrderDetailCommand
    {
        private string _connectionString;

        public CustomerOrderDetailCommand(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public IList<CustomerOrderDetailModel> GetList() 
        { 
            List<CustomerOrderDetailModel> list = new List<CustomerOrderDetailModel>();
            var sql = "CustomerOrderDetail_GetList";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                list = connection.Query<CustomerOrderDetailModel>(sql).ToList();
            }
            return list;
        }

    }
}
