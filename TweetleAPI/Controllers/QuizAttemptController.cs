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
    public class QuizAttemptController : ControllerBase
    {
        private readonly IQuizAttemptRepository _quizAttemptRepository;

        public QuizAttemptController(IQuizAttemptRepository quizAttemptRepository)
        {
            _quizAttemptRepository = quizAttemptRepository;
        }

        // GET: api/quizAttempts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizAttempt>>> Get()
        {
            var quizAttempts = await _quizAttemptRepository.GetAllQuizAttemptsAsync();
            return Ok(quizAttempts);
        }

        // GET: api/quizAttempts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizAttempt>> Get(Guid id)
        {
            var quizAttempt = await _quizAttemptRepository.GetQuizAttemptByIdAsync(id);
            if (quizAttempt == null)
            {
                return NotFound();
            }

            return Ok(quizAttempt);
        }

        // POST: api/quizAttempts
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<QuizAttempt>> Post([FromBody] QuizAttemptDto dto)
        {
            if (dto == null)
            {
                return BadRequest("QuizAttempt data cannot be null");
            }

            // Map the DTO to the QuizAttempt entity
            var newQuizAttempt = new QuizAttempt
            {
                Score = dto.Score,
            };

            var createdQuizAttempt = await _quizAttemptRepository.CreateQuizAttemptAsync(newQuizAttempt);
            return CreatedAtAction(nameof(Get), new { id = createdQuizAttempt.Id }, createdQuizAttempt);
        }

        // PUT: api/quizAttempts/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] QuizAttemptDto quizAttemptDto)
        {
            if (quizAttemptDto == null)
            {
                return BadRequest("QuizAttempt data cannot be null");
            }

            // Fetch the existing quizAttempt from the database
            var existingQuizAttempt = await _quizAttemptRepository.GetQuizAttemptByIdAsync(id);
            if (existingQuizAttempt == null)
            {
                return NotFound($"QuizAttempt with ID {id} not found");
            }

            // Update the fields from the DTO
            existingQuizAttempt.Score = quizAttemptDto.Score ?? existingQuizAttempt.Score;

            // Save the updated quizAttempt
            var updatedQuizAttempt = await _quizAttemptRepository.UpdateQuizAttemptAsync(id, existingQuizAttempt);
            if (updatedQuizAttempt == null)
            {
                return StatusCode(500, "An error occurred while updating the quizAttempt");
            }

            return NoContent();
        }

        // DELETE: api/quizAttempts/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _quizAttemptRepository.DeleteQuizAttemptAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
