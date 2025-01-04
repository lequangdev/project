using Common.DTO;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IOrderRepo : IBaseRepo<OrdersEntity>
    {

        public List<OrdersDTO> GetAllByUserID(Guid id);
        public List<OrdersDTO> GetAllDTO();
    }
}
