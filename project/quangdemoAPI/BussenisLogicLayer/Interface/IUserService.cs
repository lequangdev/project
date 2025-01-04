using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussenisLogicLayer.Interface
{
    public interface IUserService : IBaseService<UserEntity>
    {
        public object LoginUser(UserEntity user);
        public UserEntity GetUser(string user_id);
        public bool validUser(string profile);
        public dynamic RegisterUser(UserEntity user);
        
        
    }
}
