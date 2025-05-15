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
                if (model == null || string.IsNullOrEmpty(model.LoginId) || string.IsNullOrEmpty(model.Password))
                {
                    return BadRequest("Invalid Input Data.");
                }

                // Log base64 image size before storing
                if (!string.IsNullOrEmpty(model.UserImage))
                {
                    Console.WriteLine($"Received Base64 image length: {model.UserImage.Length}");

                    // Limit image size (e.g., ~2MB in Base64 is ~2.7 million characters)
                    if (model.UserImage.Length > 2700000)
                    {
                        return BadRequest("Image is too large. Please upload an image smaller than 2MB.");
                    }
                }

                UserHandler userHandler = new UserHandler();
                User existingUser = userHandler.GetUser(model.LoginId);
                if (existingUser != null)
                {
                    return BadRequest("User already exists with this login ID.");
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

                User newUser = new User
                {
                    Password = hashedPassword,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    SecurityAnswer = model.SecurityAnswer,
                    SecurityQuestion = model.SecurityQuestion,
                    City = model.City,
                    BirthDate = model.BirthDate,
                    UserImage = model.UserImage, // Base64 stored (consider cloud storage instead)
                    Name = model.Name,
                    LoginId = model.LoginId,
                };

                userHandler.AddUser(newUser);

                return Ok(new
                {
                    message = "User registered successfully.",
                    user = new
                    {
                        newUser.Name,
                        newUser.Email,
                        newUser.LoginId,
                        newUser.City,
                        newUser.PhoneNumber,
                        newUser.UserImage,
                        newUser.BirthDate,
                        newUser.SecurityQuestion,
                        newUser.SecurityAnswer
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SignUp: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                return StatusCode(500, "Internal Server Error.");
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
