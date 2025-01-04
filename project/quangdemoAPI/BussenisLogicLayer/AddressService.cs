using BussenisLogicLayer.Interface;
using Common.Model;
using DataAccessLayer;
using DataAccessLayer.Interface;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BussenisLogicLayer
{
    public class AddressService : BaseService<AddressEntity>, IAddressService
    {
        IAddressRepo _repo;
        public AddressService(IAddressRepo repo) : base(repo)
        {
            _repo = repo;
        }
        public List<AddressEntity> GetAddressUser(string user_id)
        {
            var res = _repo.GetAddressUser(user_id);
            return res;
        }

        //địa chỉ người dùng
        public override bool Insert(AddressEntity addressUser)
        {
            addressUser.address_id = Guid.NewGuid();
            var res = _repo.Insert(addressUser);
            return res;
        }
        // insert trả về id
        public dynamic InsertResID(AddressEntity addressUser)
        {
            addressUser.address_id = Guid.NewGuid();
            var res = _repo.Insert(addressUser);
            if (res)
            {
                return addressUser.address_id;
            }
            return false;
        }
        public List<AddressEntity> GetAddressUserDefault(Guid user_id)
        {
            var res = _repo.GetAddressUserDefault(user_id);
            return res;
        }

        public virtual bool UpdateByID(AddressEntity data)
        {
            _repo.UpdateAddressdefault(data);
            return _repo.UpdateByID(data);
        }

        public bool UpdateAddressdefault(AddressEntity data)
        {
            return _repo.UpdateAddressdefault(data);
        }

        public override bool DeleteByID(Guid id)
        {
            // Thực hiện xóa trước
            bool deleteResult = _repo.DeleteByID(id);
            return deleteResult;
        }
    }
}
