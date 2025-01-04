using BussenisLogicLayer.Interface;
using Common.DTO;
using Common.Model;
using Microsoft.AspNetCore.Mvc;

namespace quangdemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : BaseApi<StaffEntity>
    {
        IStaffService _service; // feild
        public StaffController(IStaffService service) : base(service)
        {
            _service = service;
        }
        [HttpGet("getAllStaff")]
        public IActionResult getAllStaff()
        {
            var res = _service.getAllStaff();
            return Ok(res);
        }
        [HttpDelete("DeleteListID")]
        public IActionResult DeleteListID([FromBody] StaffDTO listID)
        {
            var res = _service.DeleteListID(listID);
            return Ok(res);
        }
        [HttpPut("UpdateStaff")]
        public IActionResult UpdateStaff([FromBody] StaffDTO updateData)
        {
            var res = _service.UpdateStaff(updateData);
            return Ok(res);
        }
        [HttpGet("GetAllByPosition")]
        public IActionResult GetAllByPosition([FromQuery] ColumnValueDTO param)
        {
            var res = _service.GetAllByPosition(param);
            return Ok(res);
        }
    }
}
