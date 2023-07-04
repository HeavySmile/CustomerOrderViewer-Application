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
    class ItemCommand
    {
        private string _connectionString;

        public ItemCommand(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public IList<ItemModel> GetList() 
        { 
            List<ItemModel> list = new List<ItemModel>();
            var sql = "Item_GetList";
            
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                list = connection.Query<ItemModel>(sql).ToList();
            }

            return list;
        }
    }
}
