using Common.DTO;
using Common.Model;
using Dapper;
using DataAccessLayer.Interface;
using DocumentFormat.OpenXml.Office2010.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DataAccessLayer
{
    public class OrderRepo : MysqlRepo<OrdersEntity>, IOrderRepo
    {
        public List<OrdersDTO> GetAllDTO()
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            List<OrdersDTO> results = connection.Query<OrdersDTO>($"SELECT orders.price, orders.quantity,orders.status, product.product_name, product.img_name,address.*,orders.order_id, orders.product_id  FROM orders JOIN product ON orders.product_id = product.product_id JOIN address ON orders.address_id = address.address_id;").ToList();
            connection.Close();
            return results;
        }
        public List<OrdersDTO> GetAllByUserID(Guid id)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id);
            List<OrdersDTO> results = connection.Query<OrdersDTO>($"SELECT orders.price, orders.quantity,orders.status, product.product_name, product.img_name,address.*,orders.order_id, orders.product_id  FROM orders JOIN product ON orders.product_id = product.product_id JOIN address ON orders.address_id = address.address_id WHERE orders.user_id = @id AND product.product_quantity > 0;", parameters).ToList();
            connection.Close();
            return results;

        }
        public override bool DeleteByID(Guid id)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("order_id", id);
            var delSqlString = $"DELETE FROM orders WHERE @order_id = order_id;";
            var res = connection.Execute(delSqlString, parameters);
            connection.Close();
            if (res <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override bool UpdateByID(OrdersEntity data)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            try
            {
                // Lấy tên các thuộc tính của đối tượng TEntity
                var properties = typeof(OrdersEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // Tạo danh sách các cột và các tham số để cập nhật
                string updateColumns = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
                // Tạo tên của property khóa chính dựa trên _tableName
                string primaryKeyProperty = "order_id";
                // Tạo câu lệnh SQL
                string sql = @$"UPDATE orders SET {updateColumns} WHERE {primaryKeyProperty} = @{primaryKeyProperty} ";

                var result = connection.Execute(sql, data);

                return result > 0;
            }
            catch
            {
                return false;
            }
            finally { connection.Close(); }
        }
    }
}
