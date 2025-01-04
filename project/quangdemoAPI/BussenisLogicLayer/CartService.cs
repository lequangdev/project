using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussenisLogicLayer.Interface;
using Common.Model;
using DataAccessLayer;
using DataAccessLayer.Interface;
using Mysqlx.Crud;
using static Dapper.SqlMapper;
using Common.DTO;
namespace BussenisLogicLayer
{
    public class CartService : BaseService<CartEntity>, ICartService
    {
        ICartRepo _repo;
        public CartService(ICartRepo repo) : base(repo)
        {
            _repo = repo;
        }

        public override bool Insert(CartEntity models)
        {
            models.cart_id = Guid.NewGuid();
            var result = base.Insert(models);
            return result;
        }
        public List<CartDTO> GetAllByUserID(Guid id)
        {
            return _repo.GetAllByUserID(id);
        }
       
    }
}

