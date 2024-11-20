using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TweetleBLL.Services;
using TweetleModels.Dtos;
using TweetleModels.IdentityModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TweetleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenService _tokenService;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, TokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }


        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = _roleManager.Roles.ToList();

            if (roles.Count == 0)
            {
                return Ok(new List<string>());
            }

            var roleNames = roles.Select(role => role.Name).ToList();

            return Ok(roleNames);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null)
                return Unauthorized(new { message = "User not found." });

            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!passwordCheck)
                return Unauthorized(new { message = "Invalid password." });

            var roles = await _userManager.GetRolesAsync(user);

            var token = _tokenService.GenerateToken(user.Id, roles.FirstOrDefault());

            return Ok(new { token,
                user = new
                {
                    id = user.Id,
                    username = user.UserName,
                    role = roles.FirstOrDefault()
                }
            });   
        }


        [HttpPost("register-admin")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Username,
                Email = dto.Email,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                }

                await _userManager.AddToRoleAsync(user, "admin");

                return Ok(new { Status = "Success", Message = "Admin created successfully!" });
            }

            return BadRequest(new { Status = "Error", Message = "Admin creation failed!", Errors = result.Errors });
        }



        [HttpPost("register-client")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Username,
                Email = dto.Email
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("client"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("client"));
                }

                await _userManager.AddToRoleAsync(user, "client");

                return Ok(new { Status = "Success", Message = "Client created successfully!" });
            }

            return BadRequest(new { Status = "Error", Message = "User creation failed!", Errors = result.Errors });
        }

    }
}
