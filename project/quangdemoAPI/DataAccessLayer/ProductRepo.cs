using Common.Model;
using Dapper;
using DataAccessLayer.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DataAccessLayer
{
    public class ProductRepo : MysqlRepo<ProductEntity>, IProductRepo
    {
        public List<ProductEntity> GetBySearch(string payload)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            string sql = "SELECT * FROM product WHERE product_name LIKE @payload";
            var parameters = new { payload = "%" + payload + "%" }; 
            List<ProductEntity> results = connection.Query<ProductEntity>(sql, parameters).ToList();
            return results;
        }
    }
}
