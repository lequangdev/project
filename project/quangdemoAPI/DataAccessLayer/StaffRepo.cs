using Common.DTO;
using Common.Model;
using Dapper;
using DataAccessLayer.Interface;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DataAccessLayer
{
    public class StaffRepo : MysqlRepo<StaffEntity>, IStaffRepo
    {

        public List<StaffDTO> getAllStaff()
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            List<StaffDTO> res = connection.Query<StaffDTO>($"SELECT user.*,staff.*,address.*  FROM staff JOIN user ON staff.user_id = user.user_id JOIN address ON staff.address_id = address.address_id ;").ToList();
            connection.Close();
            return res;
        }
        // lấy các bản ghi qua cột
        public List<StaffDTO> GetAllByPosition(ColumnValueDTO param)
        {
            string search = param.column_search;
            string column = param.column;
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(column, param.value);
            parameters.Add(search, "%" + param.search + "%");
            var sqlAll = $"SELECT user.*,staff.*,address.*  FROM staff JOIN user ON staff.user_id = user.user_id JOIN address ON staff.address_id = address.address_id Where {search} like @{search};";
            var sqlWhere = $"SELECT user.*,staff.*,address.*  FROM staff JOIN user ON staff.user_id = user.user_id JOIN address ON staff.address_id = address.address_id Where {column} = @{column} AND {search} like @{search};";
            var sqlRole = $"SELECT user.*,staff.*,address.*  FROM staff JOIN user ON staff.user_id = user.user_id JOIN address ON staff.address_id = address.address_id Where {column} = @{column};";
            var sql = sqlWhere;
            if(param.value == "/")
            {
                sql = sqlAll;
            }
            if (param.search == "/")
            {
                sql = sqlRole;
            }
            var res = connection.Query<StaffDTO>(sql, parameters).ToList();
            connection.Close();
            return res;
        }
        public StaffEntity GetByUserID(Guid? user_id)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("user_id", user_id);
            StaffEntity res = connection.QueryFirst< StaffEntity>($"SELECT staff.*  FROM staff Where user_id = @user_id;", parameters);
            connection.Close();
            return res;
        }

    }
}
