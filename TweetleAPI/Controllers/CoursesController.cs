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
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesRepository _courseRepository;

        public CoursesController(ICoursesRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        // GET: api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> Get()
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            return Ok(courses);
        }

        // GET: api/courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> Get(Guid id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // POST: api/courses
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Course>> Post([FromBody] CourseDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Course data cannot be null");
            }

            // Map the DTO to the Course entity
            var newCourse = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                Level = dto.Level
            };

            var createdCourse = await _courseRepository.CreateCourseAsync(newCourse);
            return CreatedAtAction(nameof(Get), new { id = createdCourse.Id }, createdCourse);
        }

        // PUT: api/courses/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CourseDto courseDto)
        {
            if (courseDto == null)
            {
                return BadRequest("Course data cannot be null");
            }

            // Fetch the existing course from the database
            var existingCourse = await _courseRepository.GetCourseByIdAsync(id);
            if (existingCourse == null)
            {
                return NotFound($"Course with ID {id} not found");
            }

            // Update the fields from the DTO
            existingCourse.Title = courseDto.Title ?? existingCourse.Title;
            existingCourse.Description = courseDto.Description ?? existingCourse.Description;
            existingCourse.Level = courseDto.Level ?? existingCourse.Level;

            // Save the updated course
            var updatedCourse = await _courseRepository.UpdateCourseAsync(id, existingCourse);
            if (updatedCourse == null)
            {
                return StatusCode(500, "An error occurred while updating the course");
            }

            return NoContent();
        }

        // DELETE: api/courses/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _courseRepository.DeleteCourseAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
