using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shop.Api.MD5;
using Shop.Application.Dto;
using Shop.Application.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.Get(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        public IActionResult AddUser(UserDto user)
        {
            if (_userService.Add(user))
            {
                return CreatedAtAction("GetUser", new { id = user.Id });
            }
            return Ok("User đã tồn tại");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(UserDto user)
        {
            if (_userService.Update(user))
            {
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (_userService.Delete(id))
            {
                return NoContent();
            }
            return NotFound("Không thể xóa vì User không tồn tại");
        }
        [HttpPost("SignUp")]
        public IActionResult SignUp(UserDto userDto)
        {
            var user = _userService.GetAll().Find(x => x.Account == userDto.Account);
            if(user != null)
            {
                return Ok("Tài khoản đã tồn tại");
            }
            else
            {
                SHA256 hashMethod = SHA256.Create();
                userDto.Password = MD5Hash.GetHash(hashMethod, userDto.Password);
                if (_userService.Add(userDto))
                {
                    return NoContent();
                }
            }
            return NotFound("Không thể thêm User");
        }
        [HttpPost("Login")]
        public IActionResult Login(string account, string password)
        {
            SHA256 hashMethod = SHA256.Create();
            password = MD5Hash.GetHash(hashMethod, password);
            //lấy user trong database
            var user = _userService.GetAll().Find(x => x.Account == account
                                                    && x.Password == password);
            //nếu tồn tại user thì tạo token
            if (user != null)
            {
                //mã hóa khóa bí mật
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
                //ký vào khóa bí mật đã mã hóa
                var signingCredential = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                //tạo claims chứa thông tin bổ sung
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role,"Admin"),
                    new Claim(ClaimTypes.Name,account),
                    new Claim(ClaimTypes.MobilePhone,"0917001929"),
                };
                //tạo token với các thông số khớp với cấu hình trong file startup để validate
                var token = new JwtSecurityToken
                (
                    issuer: _configuration["Jwt:Isuser"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCredential,
                    claims: claims
                );
                //sinh ra chuỗi token với các thông số ở trên
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    user
                });
            }
            return Unauthorized();
        }
    }
}
