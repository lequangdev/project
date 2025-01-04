using Common.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussenisLogicLayer.Interface
{
    public interface IProductService : IBaseService<ProductEntity>
    {
        public List<ProductEntity> GetBySearch(string payload);
    }
}
