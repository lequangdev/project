using BussenisLogicLayer;
using BussenisLogicLayer.Interface;
using Common.DTO;
using Common.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace quangdemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApi<TEntity> : ControllerBase
    {
        IBaseService<TEntity> _service;

        public BaseApi(IBaseService<TEntity> service)
        {
            _service = service;
        }

        [HttpPost()]
        public virtual IActionResult Insert([FromBody] TEntity models)
        {
            var res = _service.Insert(models);
            return Ok(res);
        }

        [HttpGet("GetAll")]
        public virtual IActionResult GetAll()
        {
            var res = _service.GetAll();
            return Ok(res);
        }
        [HttpDelete("DeleteAll")]
        public virtual IActionResult DeleteAll()
        {
            var res = _service.DeleteAll();
            return Ok(res);
        }
        [HttpDelete()]
        public virtual IActionResult DeleteByID([FromQuery] Guid id)
        {
            var res = _service.DeleteByID(id);
            return Ok(res);
        }

        [HttpPut()]
        public virtual IActionResult UpdateByID([FromBody] TEntity data)
        {
            var res = _service.UpdateByID(data);
            return Ok(res);
        }
        [HttpGet("GetAllByID")]
        public virtual IActionResult GetAllByID( Guid id)
        {
            var res = _service.GetAllByID(id);
            return Ok(res);
        }
        [HttpPost("insertList")]
        public virtual IActionResult InsertList([FromBody] List<TEntity> models)
        {
            var res = _service.InsertList(models);
            return Ok(res);
        }
        [HttpGet("RecordByColumn")]
        public virtual IActionResult GetRecordByColumn([FromQuery] ColumnValueDTO param)
        {
            var res = _service.GetRecordByColumn(param);
            return Ok(res);
        }
        
    }
}
