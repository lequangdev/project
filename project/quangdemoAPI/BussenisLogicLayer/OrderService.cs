using Common.Model;
using DataAccessLayer;
using DataAccessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using BussenisLogicLayer.Interface;
using DataAccessLayer.Interface;
using Common.DTO;

namespace BussenisLogicLayer
{
    public class OrderService : BaseService<OrdersEntity>, IOrderService
    {
        IOrderRepo _repo;
        public OrderService(IOrderRepo repo) : base(repo)
        {
            _repo = repo;
        }

        public List<OrdersDTO> GetAllDTO()
        {
            return _repo.GetAllDTO();
        }
        public List<OrdersDTO> GetAllByUserID(Guid id)
        {
            return _repo.GetAllByUserID(id);
        }
        public override bool Insert(OrdersEntity data)
        {
            data.order_id = Guid.NewGuid();
            return base.Insert(data);
        }
        public override bool DeleteByID(Guid id)
        {
            return _repo.DeleteByID(id);
        }
        public override bool InsertList(List<OrdersEntity> data)
        {
            foreach (var item in data)
            {
                item.order_id = Guid.NewGuid();
            }
            return base.InsertList(data);
        }
    }
}
