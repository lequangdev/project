using BussenisLogicLayer;
using BussenisLogicLayer.Interface;
using Common.Model;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;


namespace quangdemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : BaseApi<CartEntity>
    {
        ICartService _service;
        public CartsController(ICartService service) : base(service)
        { 
            _service = service;
        }
        [HttpGet("GetAllByUserID")]
        public IActionResult GetAllByUserID([FromQuery] Guid user_id)
        {
            var res = _service.GetAllByUserID(user_id);
            return Ok(res);
        }
    }
}
