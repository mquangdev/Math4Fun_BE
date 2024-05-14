using Math4FunBackedn.DTO;
using Math4FunBackedn.Identity;
using Math4FunBackedn.Repositories.CourseRepo;
using Math4FunBackedn.Repositories.TokenRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Math4FunBackedn.Controllers
{
    [Authorize]
    [Route("Course")]
    public class CourseController : Controller
    {
        private ICourseRepository _courseRepository;
        private ITokenRepository _tokenRepository;
        public CourseController(ICourseRepository courseRepository, ITokenRepository tokenRepository)
        {
            _courseRepository = courseRepository;
            _tokenRepository = tokenRepository;
        }
        [HttpPost("AddCourse")]
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
        [AllowAnonymous]
        [HttpGet("GetAll")]
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
        [HttpPost("RegisterCourse")]
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
        [HttpGet("GetAllCourseByUserId")]
        public async Task<IActionResult> GetAllCourseByUserId()
        {
            string authorizationHeader = Request.Headers["Authorization"];
            try
            {
                Guid userId = await _tokenRepository.DecodeToken(authorizationHeader);
                return Ok(await _courseRepository.GetCourseByUserId(userId));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetDetailCourseByUserId")]
        public async Task<IActionResult> GetDetailCourseByUserId(Guid courseId)
        {
            string authorizationHeader = Request.Headers["Authorization"];
            try
            {
                Guid userId = await _tokenRepository.DecodeToken(authorizationHeader);
                return Ok(await _courseRepository.GetDetailCourseByUserId(userId, courseId));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
        [HttpGet("GetDetailCourse")]
        public async Task<IActionResult> GetDetailCourse(Guid courseId)
        {
            try
            {
                return Ok(await _courseRepository.DetailCourse(courseId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseDTO iUpdate)
        {
            try
            {
                return Ok(await _courseRepository.UpdateCourse(iUpdate));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCourse(Guid courseId)
        {
            try
            {
                return Ok(await _courseRepository.Delete(courseId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("LeaveCourseByUser")]
        public async Task<IActionResult> LeaveCourseByUser(Guid courseId)
        {
            string authorizationHeader = Request.Headers["Authorization"];
            try
            {
                Guid userId = await _tokenRepository.DecodeToken(authorizationHeader);
                return Ok(await _courseRepository.LeaveCourseByUser(userId, courseId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
