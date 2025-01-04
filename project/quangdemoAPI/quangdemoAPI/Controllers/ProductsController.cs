using BussenisLogicLayer;
using BussenisLogicLayer.Interface;
using Common.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

using static Dapper.SqlMapper;
namespace quangdemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApi<ProductEntity>
    {
        IProductService _service; // feild
        public ProductsController(IProductService service) : base(service)
        {
            _service = service;
        }
        [HttpGet("GetBySearch")]
        public IActionResult GetBySearch([FromQuery] string payload)
        {
            var res = _service.GetBySearch(payload);
            return Ok(res);
        }
    }

}
