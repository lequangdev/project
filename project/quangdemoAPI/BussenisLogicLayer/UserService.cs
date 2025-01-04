using Common.Model;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using DataAccessLayer.Interface;
using BussenisLogicLayer.Interface;

namespace BussenisLogicLayer
{
    public class UserService : BaseService<UserEntity>,IUserService
    {
        IUserRepo _repo;
        IConfiguration _configuration;
        public UserService(IConfiguration configuration, IUserRepo repo) : base(repo)
        {
            _configuration = configuration;
            _repo = repo;
        }
        public object LoginUser(UserEntity user)
        {

            var res = _repo.LoginUser(user);
            if (res != null)
            {
                var claims = new[]
                {
                    new Claim("fullname", res.fullname),
                    new Claim("account", res.account),
                    new Claim("email", res.email),
                    new Claim("user_role", res.user_role)
                };
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwtconfig:key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var Sectoken = new JwtSecurityToken(
                    _configuration["JwtConfig:Issuer"],
                    _configuration["JwtConfig:Issuer"],
                    claims, expires: DateTime.Now.AddDays(5),
                    signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

                var infor = new
                {
                    token,
                    res
                };
                return infor;
            }

            else
            {
                return res;
            }
        }
        public UserEntity GetUser(string user_id)
        {
            var res = _repo.GetUser(user_id);
            return res;


        }
        public bool validUser(string profile)
        {
            // Kiểm tra xem Profile có tồn tại không
            if (string.IsNullOrEmpty(profile))
            {
                // Trả về lỗi nếu không tìm thấy Profile
                return false;
            }

            // Lấy JWT từ Profile
            string jwtToken = profile.Replace("Bearer ", "");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:key"]);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                // Giải mã token
                tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public dynamic RegisterUser(UserEntity user)
        {
            if (_repo.ValidateAccount(user.account))
            {
                user.user_id = Guid.NewGuid();
                var res = _repo.RegisterUser(user);
                if (res)
                {
                    var message = "Đăng ký thành công";
                    return message;
                }
                else
                {
                    var message = "Lỗi đầu vào";
                    return message;
                }
            }
            else {
                var message = "Tài khoản đã tồn tại !";
                return message;
            }
        }
        

    }
}
