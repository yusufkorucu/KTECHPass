using KTechPassApp.Data.Entity;
using KTechPassApp.Services.Services;
using KTechPassApp.ViewModels.General;
using KTechPassApp.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace KTechPassAppAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {


        IUserService userService;
        IConfiguration configuration;
        
        public UserController(IUserService userService, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.userService = userService;
            
        }
        /// <summary>
        /// Create New User endpoint Post Request With User RequestModel
        /// </summary>
        /// <param name="UserModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public IActionResult CreateUser(User UserModel)
        {
            try
            {
                var response = userService.CreateUser(UserModel);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public IActionResult Login([FromBody] LoginRequestVM request)
        {
            var response = new Dictionary<string, string>();
            LoginResponseModel rp = new LoginResponseModel();
            var user = userService.Authenticate(request);

            if (user != null)
            {
                var token = GenerateJwtToken(user);

                return Ok(new LoginResponseModel()
                {
                    AccessToken = token,
                    IsSuccess = true,
                    Messsage="Baþarýyla giriþ yapýldý."
                }) ;
            }

            else
            {
                response.Add("Error", "Invalid username or password");
                return BadRequest(response);
            }

            
        }

        [HttpGet("Test")]
        public IActionResult Test()
        {
            var result = userService.GetCurrentUser();
            return Ok(result);
        }

      
        private string GenerateJwtToken(User request)
        {
         


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Name),
                new Claim(ClaimTypes.Email, request.Email)
                
            };

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
              configuration["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddDays(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}