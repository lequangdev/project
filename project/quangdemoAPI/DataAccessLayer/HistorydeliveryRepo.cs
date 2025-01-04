using Common.DTO;
using Common.Model;
using Dapper;
using DataAccessLayer.Interface;
using DocumentFormat.OpenXml.Office2010.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DataAccessLayer
{
    public class HistorydeliveryRepo : MysqlRepo<HistorydeliveryEntity>, IHistorydeliveryRepo
    {
        public List<HistoryDeliveryDTO> GetAllByUserID(Guid id)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id);
            List<HistoryDeliveryDTO> results = connection.Query<HistoryDeliveryDTO>($"SELECT product.*,historydelivery.*,address.*, user.* FROM historydelivery JOIN product ON historydelivery.product_id = product.product_id JOIN address ON historydelivery.address_id = address.address_id JOIN user ON user.user_id = historydelivery.user_id  WHERE historydelivery.user_id = @id;", parameters).ToList();
            connection.Close();
            return results;

        }
        public List<RevenuDTO> GetAllByTime(PeripodDTO date)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            string start = date.startingDate.ToString("yyyy-MM-dd HH:mm:ss");
            string end = date.endingDate.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = $"SELECT historydelivery.*, user.fullname, product.product_name FROM historydelivery JOIN user ON historydelivery.user_id = user.user_id JOIN product ON historydelivery.product_id = product.product_id WHERE date(delivery_date) BETWEEN '{start}' AND '{end}';";
            List<RevenuDTO> res = connection.Query<RevenuDTO>(sql).ToList();
            connection.Close();
            return res;
        }
        public List<HistoryDeliveryDTO> getStaffHistory(Guid? staff_id)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("staff_id", staff_id);
            List<HistoryDeliveryDTO> results = connection.Query<HistoryDeliveryDTO>($"SELECT product.*,historydelivery.*,address.*, user.* FROM historydelivery JOIN product ON historydelivery.product_id = product.product_id JOIN address ON historydelivery.address_id = address.address_id JOIN user ON user.user_id = historydelivery.user_id  WHERE historydelivery.staff_id = @staff_id;", parameters).ToList();
            connection.Close();
            return results;
        }
    }  
}   