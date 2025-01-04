using Common.Model;
using Dapper;
using DataAccessLayer.Interface;
using DocumentFormat.OpenXml.Spreadsheet;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccessLayer
{
    public class AddressRepo : MysqlRepo<AddressEntity>, IAddressRepo
    {
        public List<AddressEntity> GetAddressUser(string user_id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@user_id", user_id);
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            var res = connection.Query<AddressEntity>("SELECT * FROM address u WHERE @user_id = user_id;", parameters).ToList();
            connection.Close();
            return res;
        }
        // địa chỉ giao hàng người dùng
        public override bool Insert(AddressEntity addressUser)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@user_id", addressUser.user_id);
            // Kiểm tra xem user đã có địa chỉ với address_default = 2 chưa
            if (addressUser.address_default == 2)
            {
                connection.Query("UPDATE address SET address_default = 1 WHERE  @user_id = user_id;", parameters);
            }
            else
            {
                var result = connection.QueryFirstOrDefault("Select address_default from address where @user_id = user_id and address_default = 2", parameters);
                if (result == null)
                {
                    addressUser.address_default = 2;
                }
            }
            var sql = @"
            INSERT INTO address
            (
            address_id,
            address_fullName,
            address_phoneNumber,
            address_province,
            address_district,
            address_commune,
            address_village,
            address_apartment,
            user_id,
            address_default
            )
            VALUES
            (
                @address_id,
                @address_fullName,
                @address_phoneNumber,
                @address_province,
                @address_district,
                @address_commune,
                @address_village,
                @address_apartment,
                @user_id,
                @address_default
            );";
            var res = connection.Execute(sql, addressUser);
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


        public List<AddressEntity> GetAddressUserDefault(Guid user_id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@user_id", user_id);
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            var res = connection.Query<AddressEntity>("SELECT * FROM address u WHERE @user_id = user_id AND  address_default = 2;", parameters).ToList();
            connection.Close();
            return res;
        }
        // cập nhập lại địa chỉ mặc định
        public bool UpdateAddressdefault(AddressEntity data)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("user_id", data.user_id);
                parameters.Add("address_id", data.address_id);
                // Đặt tất cả địa chỉ của người dùng này thành không mặc định.
                var resetSql = "UPDATE address SET address_default = 1 WHERE user_id = @user_id;";
                connection.Execute(resetSql, parameters);
                // Đặt địa chỉ được chọn thành mặc định.
                var updateSql = "UPDATE address SET address_default = 2 WHERE address_id = @address_id;";
                var res = connection.Execute(updateSql, parameters);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }


        }
        // xóa một bản ghi kèm kiểm tra xem có xóa vào địa chỉ mặc định không
        public virtual bool DeleteByID(Guid address_id)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("address_id", address_id);
            var sqluser_id = "SELECT user_id FROM address u WHERE u.address_id = @address_id;";
            var user_id = connection.QueryFirstOrDefault<AddressEntity>(sqluser_id, parameters);
            var checkAr = 1;
            var delSqlString = $"DELETE FROM address WHERE @address_id = address_id;";
            var res = connection.Execute(delSqlString, parameters);
            var sqladdress = "SELECT address_default FROM address u WHERE u.user_id = @user_id;";
            var data = connection.Query<AddressEntity>(sqladdress, new { user_id.user_id }).ToList();
            foreach (var item in data)
            {
                if (item.address_default == 1)
                {
                    checkAr = 1;
                }
                else
                {
                    checkAr = 2;
                    break;
                }
            }
            if (checkAr == 2)
            {
                connection.Close();
                return true;
            }
            else
            {
                // lấy ra bản ghi đầu tiên của trường địa chỉ
                var sqlGetFirstAddress = "SELECT address_id FROM address WHERE user_id = @user_id ORDER BY address_id LIMIT 1;";
                var firstAddress = connection.QueryFirstOrDefault<Guid>(sqlGetFirstAddress, new { user_id.user_id });
                // thay đổi giá trị bản ghi đầu tiên của trường địa chỉ
                var sqlAddress_default = "UPDATE address SET address_default = 2 WHERE address_id = @firstAddress;";
                connection.Execute(sqlAddress_default, new { firstAddress });
            }
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
    }
}
