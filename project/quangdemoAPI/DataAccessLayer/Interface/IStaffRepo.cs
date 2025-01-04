using Common.DTO;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DataAccessLayer.Interface
{
    public interface IStaffRepo : IBaseRepo<StaffEntity>
    {
        public List<StaffDTO> getAllStaff();
        public List<StaffDTO> GetAllByPosition(ColumnValueDTO param);
        public StaffEntity GetByUserID(Guid? user_id);
    }
}
    