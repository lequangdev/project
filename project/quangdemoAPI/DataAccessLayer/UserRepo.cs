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
    public class UserRepo : MysqlRepo<UserEntity>, IUserRepo
    {
        public UserEntity LoginUser(UserEntity user)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@account", user.account);
            parameters.Add("@password", user.password);
            UserEntity res = connection.QueryFirstOrDefault<UserEntity>("SELECT * FROM user u where account = @account AND password = @password;", parameters);
            connection.Close();
            if (res == null)
            {
                return null;
            }
            else
            {
                return res;
            }

        }
        public UserEntity GetUser(string user_id) {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("user_id", user_id);

            UserEntity res = connection.QueryFirstOrDefault<UserEntity>("SELECT * FROM user u where user_id = @user_id;", parameters);
            connection.Close();
            return res;
        }
        public bool ValidateAccount(string account)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("account", account);
            UserEntity res = connection.QueryFirstOrDefault<UserEntity>("SELECT * FROM user u where account = @account;", parameters);
            connection.Close();
            if (res != null)
            {
                return false;
            }
            return true;
        }
        public bool RegisterUser(UserEntity user)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            var sql = @"
            INSERT INTO USER
            (   
            user_id,
            fullname,  
            email,
            phone_number,
            account ,
            password,
            avatar_url,
            user_role
            )
            VALUES
            (
                @user_id,
                @fullname, 
                @email,
                @phone_number,
                @account ,
                @password,
                @avatar_url,
                @user_role
            );";
            var res = connection.Execute(sql, user);
            connection.Close();
            if (res > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
     
    }
}
