using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IAddressRepo : IBaseRepo<AddressEntity>
    {
        public List<AddressEntity> GetAddressUser(string user_id);

        public List<AddressEntity> GetAddressUserDefault(Guid user_id);
        public bool UpdateAddressdefault(AddressEntity data);
    }
}
