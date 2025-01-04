using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IUserRepo: IBaseRepo<UserEntity>
    {
       
        public UserEntity LoginUser(UserEntity user);
        public UserEntity GetUser(string user_id);
        public bool RegisterUser(UserEntity user);
        public bool ValidateAccount(string account);
    }
}
