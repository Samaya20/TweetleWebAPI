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
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        // GET: api/questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> Get()
        {
            var questions = await _questionRepository.GetAllQuestionsAsync();
            return Ok(questions);
        }

        // GET: api/questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> Get(Guid id)
        {
            var question = await _questionRepository.GetQuestionByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // POST: api/questions
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Question>> Post([FromBody] QuestionDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Question data cannot be null");
            }

            // Map the DTO to the Question entity
            var newQuestion = new Question
            {
                Content = dto.Content,
                Option1 = dto.Option1,
                Option2 = dto.Option2,
                Option3 = dto.Option3,
                Option4 = dto.Option4,
                CorrectAnswer = dto.CorrectAnswer
            };

            var createdQuestion = await _questionRepository.CreateQuestionAsync(newQuestion);
            return CreatedAtAction(nameof(Get), new { id = createdQuestion.Id }, createdQuestion);
        }

        // PUT: api/questions/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] QuestionDto questionDto)
        {
            if (questionDto == null)
            {
                return BadRequest("Question data cannot be null");
            }

            // Fetch the existing question from the database
            var existingQuestion = await _questionRepository.GetQuestionByIdAsync(id);
            if (existingQuestion == null)
            {
                return NotFound($"Question with ID {id} not found");
            }

            // Update the fields from the DTO
            existingQuestion.Content = questionDto.Content ?? existingQuestion.Content;
            existingQuestion.Option1 = questionDto.Option1 ?? existingQuestion.Option1;
            existingQuestion.Option2 = questionDto.Option2 ?? existingQuestion.Option2;
            existingQuestion.Option3 = questionDto.Option3 ?? existingQuestion.Option3;
            existingQuestion.Option4 = questionDto.Option4 ?? existingQuestion.Option4;
            existingQuestion.CorrectAnswer = questionDto.CorrectAnswer ?? existingQuestion.CorrectAnswer;

            // Save the updated question
            var updatedQuestion = await _questionRepository.UpdateQuestionAsync(id, existingQuestion);
            if (updatedQuestion == null)
            {
                return StatusCode(500, "An error occurred while updating the question");
            }

            return NoContent();
        }

        // DELETE: api/questions/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _questionRepository.DeleteQuestionAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
