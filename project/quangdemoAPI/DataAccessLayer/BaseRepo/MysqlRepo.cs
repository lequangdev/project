using Common.DTO;
using Common.Model;
using Dapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using Mysqlx;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DataAccessLayer
{
    public class MysqlRepo<TEntity> : IBaseRepo<TEntity>
    {
        string _tableName = "";
        public MysqlRepo()
        {
            _tableName = GetTableName(typeof(TEntity).Name);
        }
        public static string GetTableName(string tableName)
        {
            string suffix = "Entity";
            if (tableName.EndsWith(suffix))
            {
                return tableName.Substring(0, tableName.Length - suffix.Length);
            }
            return tableName;
        }
        //Thêm sản phẩm vào db
        public virtual bool Insert(TEntity models)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            try
            {
                // Lấy tên các thuộc tính của đối tượng TEntity
                var properties = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // Tạo danh sách các cột và các tham số+
                string columns = string.Join(", ", properties.Select(p => p.Name));
                string parameters = string.Join(", ", properties.Select(p => "@" + p.Name));
                // Tạo câu lệnh SQL
                string sql = @$"
                    INSERT INTO {_tableName}
                    (
                        {columns}
                    )
                    VALUES
                    (
                        {parameters}
                    );";

                var result = connection.Execute(sql, models);
                return result > 0;
            }
            catch
            {
                return false;
            }
            finally { connection.Close(); }
        }
        // thêm một list
        public virtual bool InsertList(List<TEntity> models)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            try
            {
                // Lấy tên các thuộc tính của đối tượng TEntity
                var properties = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // Tạo danh sách các cột và các tham số
                string columns = string.Join(", ", properties.Select(p => p.Name));
                string parameters = string.Join(", ", properties.Select(p => "@" + p.Name));
                // Tạo câu lệnh SQL
                string sql = @$"
                    INSERT INTO {_tableName}
                    (
                        {columns}
                    )
                    VALUES
                    (
                        {parameters}
                    );";

                var result = connection.Execute(sql, models);

                return result > 0;
            }
            catch
            {
                return false;
            }
            finally { connection.Close(); }
        }
        //Lấy sản phẩm ra trả về client
        public virtual List<TEntity> GetAll()
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            List<TEntity> results = connection.Query<TEntity>($"SELECT * FROM {_tableName} u").ToList();
            return results;
        }
        // xóa tất cả bản ghi
        public virtual bool DeleteAll()
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            var delSqlString = $"DELETE FROM {_tableName};";
            var res = connection.Execute(delSqlString);
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
        // xóa bản ghi dựa trên id bảng đó
        public virtual bool DeleteByID(Guid id)
        {
            string tableID = _tableName + "_id";
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(tableID, id);
            var delSqlString = $"DELETE FROM {_tableName} WHERE @{tableID} = {tableID};";
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


        public virtual bool UpdateByID(TEntity data)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            try
            {
                // Lấy tên các thuộc tính của đối tượng TEntity
                var properties = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // Tạo danh sách các cột và các tham số để cập nhật
                string updateColumns = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
                // Tạo tên của property khóa chính dựa trên _tableName
                string primaryKeyProperty = $"{_tableName}_id";
                // Tạo câu lệnh SQL
                string sql = @$"UPDATE {_tableName} SET {updateColumns} WHERE {primaryKeyProperty} = @{primaryKeyProperty} ";

                var result = connection.Execute(sql, data);

                return result > 0;
            }
            catch
            {
                return false;
            }
            finally { connection.Close(); }
        }

        public virtual List<TEntity> GetAllByID(Guid? id)
        {
            string tableID = _tableName + "_id";
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(tableID, id);
            var res = connection.Query<TEntity>($"SELECT * FROM {_tableName} u WHERE @{tableID} = {tableID};", parameters).ToList();
            connection.Close();
            return res;
        }
        // xóa bản ghi với bảng được chọn
        public virtual bool DeleteByTable(Guid? id, string table)
        {
            string tableID = table + "_id";
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(tableID, id);
            var delSqlString = $"DELETE FROM {_tableName} WHERE @{tableID} = {tableID};";
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
        // lấy các bản ghi qua cột
        public virtual List<TEntity> GetRecordByColumn(ColumnValueDTO param)
        {
            string search = param.column_search;
            string column = param.column;
            MySqlConnection connection = new MySqlConnection("server=localhost;Port=3307;user=root;password=12345678;database=quangdeptrai");
            connection.Open();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add(column, param.value);
            parameters.Add(search, "%" + param.search + "%");
            var sqlAll = $"SELECT * FROM {_tableName} u WHERE @{column} = {column};";
            var sqlSearch = $"SELECT * FROM {_tableName} u WHERE {search} LIKE @{search};";
            var sql = sqlAll;
            if (param.value == "/")
            {
                sql = sqlSearch;
            }
            var res = connection.Query<TEntity>(sql, parameters).ToList();
            connection.Close();
            return res;
        }

    }
}
