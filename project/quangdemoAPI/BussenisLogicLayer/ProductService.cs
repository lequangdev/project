using BussenisLogicLayer.Interface;
using Common.Model;
using DataAccessLayer;
using DataAccessLayer.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BussenisLogicLayer
{
    public class ProductService : BaseService<ProductEntity>, IProductService
    {
        IProductRepo _repo;
        public ProductService(IProductRepo repo) : base(repo)
        {
            _repo = repo;
        }
        public override bool Insert(ProductEntity models)
        {
            models.product_id = Guid.NewGuid();
            return base.Insert(models);
        }
        public List<ProductEntity> GetBySearch(string payload)
        {
            return _repo.GetBySearch(payload);
        }
    }
}
