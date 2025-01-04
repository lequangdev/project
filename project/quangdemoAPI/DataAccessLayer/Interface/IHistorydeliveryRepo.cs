using Common.DTO;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Interface
{
    public interface IHistorydeliveryRepo : IBaseRepo<HistorydeliveryEntity>
    {
        public List<HistoryDeliveryDTO> GetAllByUserID(Guid id);
        public List<RevenuDTO> GetAllByTime(PeripodDTO date);
        public List<HistoryDeliveryDTO> getStaffHistory(Guid? staff_id);

    }
}
