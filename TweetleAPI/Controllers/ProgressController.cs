using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TweetleDAL.Interfaces;
using TweetleModels.Dtos;
using TweetleModels.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TweetleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly IProgressRepository _progressRepository;

        public ProgressController(IProgressRepository progressRepository)
        {
            _progressRepository = progressRepository;
        }

        // GET: api/progresss
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Progress>>> Get()
        {
            var progresss = await _progressRepository.GetAllProgressesAsync();
            return Ok(progresss);
        }

        // GET: api/progresss/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Progress>> Get(Guid id)
        {
            var progress = await _progressRepository.GetProgressByIdAsync(id);
            if (progress == null)
            {
                return NotFound();
            }

            return Ok(progress);
        }

        // POST: api/progresss
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Progress>> Post([FromBody] ProgressDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Progress data cannot be null");
            }

            // Map the DTO to the Progress entity
            var newProgress = new Progress
            {
                CompletedLessons = dto.CompletedLessons
            };

            var createdProgress = await _progressRepository.CreateProgressAsync(newProgress);
            return CreatedAtAction(nameof(Get), new { id = createdProgress.Id }, createdProgress);
        }

        // PUT: api/progresss/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ProgressDto progressDto)
        {
            if (progressDto == null)
            {
                return BadRequest("Progress data cannot be null");
            }

            // Fetch the existing progress from the database
            var existingProgress = await _progressRepository.GetProgressByIdAsync(id);
            if (existingProgress == null)
            {
                return NotFound($"Progress with ID {id} not found");
            }

            // Update the fields from the DTO
            existingProgress.CompletedLessons = progressDto.CompletedLessons ?? existingProgress.CompletedLessons;

            // Save the updated progress
            var updatedProgress = await _progressRepository.UpdateProgressAsync(id, existingProgress);
            if (updatedProgress == null)
            {
                return StatusCode(500, "An error occurred while updating the progress");
            }

            return NoContent();
        }

        // DELETE: api/progresss/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _progressRepository.DeleteProgressAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
