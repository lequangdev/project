using Common.Model;
using Microsoft.AspNetCore.Mvc;
using BussenisLogicLayer.Interface;
using Common.DTO;

namespace quangdemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorydeliveryController : BaseApi<HistorydeliveryEntity>
    {
        IHistorydeliveryService _service;
        public HistorydeliveryController(IHistorydeliveryService service) : base(service)
        {
            _service = service;
        }
        [HttpGet("GetAllByUserID")]
        public IActionResult GetAllByUserID([FromQuery] Guid id)
        {
            var res = _service.GetAllByUserID(id);
            return Ok(res);
        }
        [HttpGet("GetAllByTime")]
        public IActionResult GetAllByTime([FromQuery] PeripodDTO date)
        {
            var res = _service.GetAllByTime(date);
            return Ok(res);
        }
        [HttpGet("getStaffHistory")]
        public IActionResult getStaffHistory([FromQuery] Guid staff_id)
        {
            var res = _service.getStaffHistory(staff_id);
            return Ok(res);
        }
    }
}
