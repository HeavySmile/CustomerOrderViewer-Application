using CustomerOrderViewer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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

        public void Delete(int customerOrderId, string uid)
        {
            var sql = "CustomerOrderDetail_Delete";

            using (SqlConnection connection = new SqlConnection(_connectionString)) 
            {
                connection.Execute(sql, new { @CustomerOrderId = customerOrderId, @UserId = uid }, commandType: CommandType.StoredProcedure);
            }
        }

        public void Upsert(int customerOrderId, int customerId, int itemId, string uid)
        {
            var sql = "CustomerOrderDetail_Upsert";

            var upsertTable = new DataTable();
            upsertTable.Columns.Add("CustomerOrderId", typeof(int));
            upsertTable.Columns.Add("CustomerId", typeof(int));
            upsertTable.Columns.Add("ItemId", typeof(int));
            upsertTable.Rows.Add(customerOrderId, customerId, itemId);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new { @CustomerOrderType = upsertTable.AsTableValuedParameter("CustomerOrderType"), @UserId = uid }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
