using BussenisLogicLayer.Interface;
using Common.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace quangdemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : BaseApi<AddressEntity>
    {
        IAddressService _service;
        public AddressController(IAddressService service) : base(service)
        {
            _service = service;
        }
        // Lấy ra danh sách địa chỉ người dùng đã thêm
        [HttpGet("GetAddressUser")]
        public IActionResult GetAddressUser([FromQuery] string user_id)
        {
            var res = _service.GetAddressUser(user_id);
            return Ok(res);
        }

        // lấy ra địa chỉ mặc định
        [HttpGet("GetAddressDefault")]
        public IActionResult GetAddressDefault([FromQuery] Guid user_id)
        {
            var res = _service.GetAddressUserDefault(user_id);
            return Ok(res);
        }
        // sửa địa chỉ mặc định người dùng
        [HttpPut("UpdateAddressdefault")]
        public IActionResult UpdateAddressdefault([FromBody] AddressEntity data)
        {
            var res = _service.UpdateAddressdefault(data);
            return Ok(res);
        }
        [HttpPost("InsertResID")]
        public IActionResult InsertResID([FromBody] AddressEntity addressUser)
        {
            var res = _service.InsertResID(addressUser);
            return Ok(res);
        }
    }
}
