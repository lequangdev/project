using Common.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using Common.DTO;
namespace DataAccessLayer.Interface
{
    public interface ICartRepo : IBaseRepo<CartEntity>
    {
        public List<CartDTO> GetAllByUserID(Guid id);
    }
}
