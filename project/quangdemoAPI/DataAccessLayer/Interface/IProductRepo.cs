using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{
    public interface IProductRepo: IBaseRepo<ProductEntity>
    {
        public List<ProductEntity> GetBySearch(string payload);
    }
}
