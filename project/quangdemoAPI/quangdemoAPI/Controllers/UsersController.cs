using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Dapper;
using System.Security.Principal;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using static Dapper.SqlMapper;
using Common.Model;
using BussenisLogicLayer;
using Mysqlx;
using BussenisLogicLayer.Interface;

namespace quangdemoAPI.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApi<UserEntity>
    {
        IUserService _service;
        public UsersController( IUserService service): base(service)
        {
            _service = service;
        }
      
        // kiểm tra tài khoản mật khẩu có hợp lệ hay không
        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] UserEntity user)
        {
            var res = _service.LoginUser(user);
            if (res == null)
            {
                return StatusCode(404, "Tài khoản hoặc mật khẩu không chính xác");
            }
            else
            {
                return Ok(res);
            }
        }
        [HttpGet("{user_id}")]
        public IActionResult GetUser([FromRoute] string user_id)
        {
            var res = _service.GetUser(user_id);
            return Ok(res);
        }
        [HttpPost("ProfileUser")]
        public IActionResult validjwtUser([FromHeader(Name = "Profile")] string profileHeader)
        {
            var res = _service.validUser(profileHeader);
            if (res)
            {
                return StatusCode(200);
            }
            else
            {
                return StatusCode(404);
            }
        }
        [HttpPost("Register")]
        public IActionResult InsertUser([FromBody] UserEntity user)
        {
            var res = _service.RegisterUser(user);
            return Ok(res);

        }
       
    
    }
}

