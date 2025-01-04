using Common.DTO;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussenisLogicLayer.Interface
{
    public interface IOrderService : IBaseService<OrdersEntity>
    {
        public List<OrdersDTO> GetAllByUserID(Guid id);
        public List<OrdersDTO> GetAllDTO();
    }
}
