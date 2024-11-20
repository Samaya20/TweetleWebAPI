using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TweetleDAL.Interfaces;
using TweetleModels.Dtos;
using TweetleModels.IdentityModels;
//using TweetleBLL.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TweetleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> Get()
        {
            var users = await _userRepository.GetAllApplicationUsersAsync();
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> Get(Guid id)
        {
            var user = await _userRepository.GetApplicationUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/users
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> Post([FromBody] UserDto dto)
        {
            if (dto == null)
            {
                return BadRequest("ApplicationUser data cannot be null");
            }

            // Map the DTO to the ApplicationUser entity
            var newApplicationUser = new ApplicationUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = dto.Password
            };

            var createdApplicationUser = await _userRepository.CreateApplicationUserAsync(newApplicationUser);
            return CreatedAtAction(nameof(Get), new { id = createdApplicationUser.Id }, createdApplicationUser);
        }

        // PUT: api/users/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("ApplicationUser data cannot be null");
            }

            // Fetch the existing user from the database
            var existingApplicationUser = await _userRepository.GetApplicationUserByIdAsync(id);
            if (existingApplicationUser == null)
            {
                return NotFound($"ApplicationUser with ID {id} not found");
            }

            // Update the fields from the DTO
            existingApplicationUser.UserName = userDto.UserName ?? existingApplicationUser.UserName;
            existingApplicationUser.Email = userDto.Email ?? existingApplicationUser.Email;
            existingApplicationUser.PasswordHash = userDto.Password ?? existingApplicationUser.PasswordHash;

            // Save the updated user
            var updatedApplicationUser = await _userRepository.UpdateApplicationUserAsync(id, existingApplicationUser);
            if (updatedApplicationUser == null)
            {
                return StatusCode(500, "An error occurred while updating the user");
            }

            return NoContent();
        }

        // DELETE: api/users/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _userRepository.DeleteApplicationUserAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
