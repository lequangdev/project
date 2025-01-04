using BussenisLogicLayer.Interface;
using Common.DTO;
using Common.Model;
using DataAccessLayer;
using DataAccessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BussenisLogicLayer
{
    public class StaffService : BaseService<StaffEntity>, IStaffService
    {
        IStaffRepo _repo;
        IUserRepo _userRepo;
        IAddressRepo _addressRepo;
        public StaffService(IStaffRepo repo, IUserRepo userRepo, IAddressRepo addressRepo) : base(repo)
        {
            _repo = repo;
            _userRepo = userRepo;
            _addressRepo = addressRepo;
        }
        public bool Insert(StaffEntity models)
        {
            models.staff_id = Guid.NewGuid();
            return base.Insert(models);
        }
        public List<StaffDTO> getAllStaff()
        {
            List<StaffDTO> res = _repo.getAllStaff();
            foreach (StaffDTO model in res) {
                model.account = null;
                model.password = null;
            }
            return res;
        }
        public bool DeleteListID(StaffDTO listID)
        {
            try
            {
                _repo.DeleteByTable(listID.user_id, "user");
                _repo.DeleteByTable(listID.address_id, "address");
                _repo.DeleteByTable(listID.staff_id, "staff");
            }
            catch
            {
                return false;
            }
            return true;
               
        }
        public bool UpdateStaff(StaffDTO updateData)
        {
            try
            {
                var UserToUpdate = _userRepo.GetAllByID(updateData.user_id);
                UserToUpdate[0].email = updateData.email;
                UserToUpdate[0].fullname = updateData.fullname;
                UserToUpdate[0].user_id = updateData.user_id;
                UserToUpdate[0].avatar_url = updateData.staff_imgUrl;
                UserToUpdate[0].user_role = updateData.user_role;
                UserToUpdate[0].phone_number = updateData.phone_number;
                var addressToUpdate = new AddressEntity
                {
                    address_id = updateData.address_id,
                    address_fullName = updateData.address_fullName,
                    address_phoneNumber = updateData.phone_number,
                    address_province = updateData.address_province,
                    address_district = updateData.address_district,
                    address_commune = updateData.address_commune,
                    address_village = updateData.address_village,
                    address_apartment = updateData.address_apartment,
                    user_id = updateData.user_id,
                    address_default = updateData.address_default 
                };
                var staffToUpdate = new StaffEntity
                {
                    staff_id = updateData.staff_id,
                    user_id = updateData.user_id,
                    address_id = updateData.address_id,
                    staff_role = updateData.staff_role,
                    staff_dateOfBirth = updateData.staff_dateOfBirth,
                    staff_identification = updateData.staff_identification,
                    staff_imgUrl = updateData.staff_imgUrl,
                };
                // update table
                _userRepo.UpdateByID(UserToUpdate[0]);
                _addressRepo.UpdateByID(addressToUpdate);
                _repo.UpdateByID(staffToUpdate);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public List<StaffDTO> GetAllByPosition(ColumnValueDTO param)
        {
            return  _repo.GetAllByPosition(param);
        }

    }
}
