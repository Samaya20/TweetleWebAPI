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
    public class QuizController : ControllerBase
    {
        private readonly IQuizRepository _quizRepository;

        public QuizController(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        // GET: api/quizs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> Get()
        {
            var quizzes = await _quizRepository.GetAllQuizsAsync();
            return Ok(quizzes);
        }

        // GET: api/quizs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> Get(Guid id)
        {
            var quiz = await _quizRepository.GetQuizByIdAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(quiz);
        }

        // POST: api/quizs
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Quiz>> Post([FromBody] QuizDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Quiz data cannot be null");
            }

            // Map the DTO to the Quiz entity
            var newQuiz = new Quiz
            {
                Title = dto.Title,
            };

            var createdQuiz = await _quizRepository.CreateQuizAsync(newQuiz);
            return CreatedAtAction(nameof(Get), new { id = createdQuiz.Id }, createdQuiz);
        }

        // PUT: api/quizs/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] QuizDto quizDto)
        {
            if (quizDto == null)
            {
                return BadRequest("Quiz data cannot be null");
            }

            // Fetch the existing quiz from the database
            var existingQuiz = await _quizRepository.GetQuizByIdAsync(id);
            if (existingQuiz == null)
            {
                return NotFound($"Quiz with ID {id} not found");
            }

            // Update the fields from the DTO
            existingQuiz.Title = quizDto.Title ?? existingQuiz.Title;

            // Save the updated quiz
            var updatedQuiz = await _quizRepository.UpdateQuizAsync(id, existingQuiz);
            if (updatedQuiz == null)
            {
                return StatusCode(500, "An error occurred while updating the quiz");
            }

            return NoContent();
        }

        // DELETE: api/quizs/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _quizRepository.DeleteQuizAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
