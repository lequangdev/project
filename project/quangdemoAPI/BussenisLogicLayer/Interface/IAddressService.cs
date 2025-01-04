using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussenisLogicLayer.Interface
{
    public interface IAddressService : IBaseService<AddressEntity>
    {
        public List<AddressEntity> GetAddressUser(string user_id);
        public List<AddressEntity> GetAddressUserDefault(Guid user_id);
        public bool UpdateAddressdefault(AddressEntity data);
        public dynamic InsertResID(AddressEntity addressUser);


    }
}
