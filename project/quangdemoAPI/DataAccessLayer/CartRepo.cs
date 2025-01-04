using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using MySql.Data.MySqlClient;
using Dapper;
using DataAccessLayer.Interface;
using static Dapper.SqlMapper;
using Common.DTO;

namespace DataAccessLayer
{
    public class CartRepo : MysqlRepo<CartEntity>, ICartRepo
    {
        public List<CartDTO> GetAllByUserID(Guid id)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id);
            List<CartDTO> results = connection.Query<CartDTO>($"SELECT product.*,cart.quantity,cart.user_id,cart.cart_id FROM cart JOIN product ON cart.product_id = product.product_id WHERE cart.user_id = @id And product.product_quantity > 0;", parameters).ToList();
            connection.Close();
            return results;

        }
    }
}


