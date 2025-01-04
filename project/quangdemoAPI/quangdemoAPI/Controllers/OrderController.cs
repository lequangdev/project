using BussenisLogicLayer.Interface;
using Common.Model;
using Microsoft.AspNetCore.Mvc;

namespace quangdemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseApi<OrdersEntity>
    {
        IOrderService _service;
        public OrderController(IOrderService service) : base(service)
        {
            _service = service;
        }
        [HttpGet("GetAllByUserID")]
        public IActionResult GetAllByUserID([FromQuery] Guid id)
        {
            var res = _service.GetAllByUserID(id);
            return Ok(res);
        }
        [HttpGet("GetAllDTO")]
        public IActionResult GetAllDTO()
        {
            var res = _service.GetAllDTO();
            return Ok(res);
        }

    }
}
