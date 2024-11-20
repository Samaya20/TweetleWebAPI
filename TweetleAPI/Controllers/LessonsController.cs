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
    public class LessonsController : ControllerBase
    {
        private readonly ILessonsRepository _lessonRepository;

        public LessonsController(ILessonsRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        // GET: api/lessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> Get()
        {
            var lessons = await _lessonRepository.GetAllLessonsAsync();
            if (lessons == null)
            {
                return BadRequest("No lesson data!");
            }
            return Ok(lessons);
        }

        // GET: api/lessons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> Get(Guid id)
        {
            var lesson = await _lessonRepository.GetLessonByIdAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }

            return Ok(lesson);
        }

        // POST: api/lessons
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Lesson>> Post([FromBody] LessonDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Lesson data cannot be null");
            }

            // Map the DTO to the Lesson entity
            var newLesson = new Lesson
            {
                Title = dto.Title,
                Content = dto.Content,
            };

            var createdLesson = await _lessonRepository.CreateLessonAsync(newLesson);
            return CreatedAtAction(nameof(Get), new { id = createdLesson.Id }, createdLesson);
        }

        // PUT: api/lessons/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] LessonDto lessonDto)
        {
            if (lessonDto == null)
            {
                return BadRequest("Lesson data cannot be null");
            }

            // Fetch the existing lesson from the database
            var existingLesson = await _lessonRepository.GetLessonByIdAsync(id);
            if (existingLesson == null)
            {
                return NotFound($"Lesson with ID {id} not found");
            }

            // Update the fields from the DTO
            existingLesson.Title = lessonDto.Title ?? existingLesson.Title;
            existingLesson.Content = lessonDto.Content ?? existingLesson.Content;

            // Save the updated lesson
            var updatedLesson = await _lessonRepository.UpdateLessonAsync(id, existingLesson);
            if (updatedLesson == null)
            {
                return StatusCode(500, "An error occurred while updating the lesson");
            }

            return NoContent();
        }

        // DELETE: api/lessons/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _lessonRepository.DeleteLessonAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
