using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
namespace BussenisLogicLayer.Interface
{
    public interface ICartService : IBaseService<CartEntity>
    {
        //public List<ProductEntity> InsertProduct();
        public List<CartDTO> GetAllByUserID(Guid id);
    }
}
