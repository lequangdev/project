using Common.DTO;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BussenisLogicLayer.Interface
{
    public interface IStaffService : IBaseService<StaffEntity>
    {
        public List<StaffDTO> getAllStaff();
        public bool DeleteListID(StaffDTO listID);
        public bool UpdateStaff(StaffDTO updateData);
        public List<StaffDTO> GetAllByPosition(ColumnValueDTO param);

    }
}
