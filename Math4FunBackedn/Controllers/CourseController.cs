using Math4FunBackedn.DTO;
using Math4FunBackedn.Repositories.CourseRepo;
using Microsoft.AspNetCore.Mvc;

namespace Math4FunBackedn.Controllers
{
    public class CourseController : Controller
    {
        private ICourseRepository _courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        [HttpPost("Course/AddCourse")]
        public async Task<IActionResult> AddCourse([FromBody] AddCourseDTO iCourse)
        {
            try
            {
                return Ok(await _courseRepository.AddCourse(iCourse));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpGet("Course/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _courseRepository.GetAllCourse());
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("Course/RegisterCourse")]
        public async Task<IActionResult> RegisterCourse([FromBody] RegisterCourseDTO iRegister)
        {
            try
            {
                return Ok(await _courseRepository.RegisterCourse(iRegister));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("Course/GetCourseByUserId")]
        public async Task<IActionResult> GetCourseByUserId(Guid UserId)
        {
            try
            {
                return Ok(await _courseRepository.GetCourseByUserId(UserId));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("Course/GetDetailCourse")]
        public async Task<IActionResult> GetDetailCourse(Guid userId, Guid courseId)
        {
            try
            {
                return Ok(await _courseRepository.GetDetailCourse(userId, courseId));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
    }
}
