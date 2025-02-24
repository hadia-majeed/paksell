using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using paksell;
using paksell.Db;
using PakSell.Models;
using Org.BouncyCastle.Crypto.Generators;


namespace PakSell.RestApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase

    {

        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
     

        [HttpGet]

        public IActionResult Get()
        {
            try
            {
                //ModelHelper.
                List<User> users = null;
                if (string.IsNullOrEmpty(Request.QueryString.Value))
                {
                    users = new UserHandler().GetUsers();
                }
                return Ok(users);
            }

            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpPost("/login")]
        public IActionResult Login(LoginModel model)
        {
            User user = new UserHandler().GetUser(model.LoginId, model.Password);
            if (user == null) {
                return BadRequest("Invalid LoginId or Password");
            }
            string token = IssueToken(user);

            return Ok(new { token = token, user = user });

        }

        [HttpPost("/signup")]
        public IActionResult SignUp(UserModel model)
        {
            try
            {
                if (model==null || string.IsNullOrEmpty(model.LoginId) || string.IsNullOrEmpty(model.Password))
                {
                    return BadRequest("invalid Input Data");
                }
                UserHandler userHandler = new UserHandler();
                User existingUser = userHandler.GetUser(model.Id);
                if (existingUser != null)
                {
                    return BadRequest("User already exists with this login ID.");
                }
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

                User newUser = new User
                {
                    Id = model.Id,
                    Password = hashedPassword,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    SecurityAnswer = model.SecurityAnswer,
                    SecurityQuestion = model.SecurityQuestion,
                    City = model.City,
                    BirthDate = model.BirthDate,
                    UserImage = model.UserImage,
                    Name = model.Name,
                    LoginId = model.LoginId,
                };
                userHandler.AddUser(newUser).ToModel();
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        private string IssueToken(User user)
        {
            string jwtKey = _configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentNullException("Jwt:Key is missing or null in appsettings.json.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim("User_Id", user.Id.ToString()),
        new Claim("loginId", user.LoginId),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
