using Math4FunBackedn.DTO;
using Math4FunBackedn.Repositories.CourseRepo;
using Math4FunBackedn.Repositories.LessonRepo;
using Microsoft.AspNetCore.Mvc;

namespace Math4FunBackedn.Controllers
{
    [Route("Lesson")]
    public class LessonController : Controller
    {
        private ILessonRepository _lessonRepository;
        public LessonController(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddLesson([FromBody] AddLessonDTO iAdd)
        {
            try
            {
                return Ok(await _lessonRepository.AddLesson(iAdd));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return Ok(await _lessonRepository.DeleteLesson(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpGet("Detail")]
        public async Task<IActionResult> Detail(Guid id)
        {
            try
            {
                return Ok(await _lessonRepository.Detail(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateLessonDTO iUpdate)
        {
            try
            {
                return Ok(await _lessonRepository.Update(iUpdate));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPost("UpdateStatusLessonByUser")]
        public async Task<IActionResult> UpdateStatusLessonByUser([FromBody] UserUpdateLessonDTO iUpdate)
        {
            try
            {
                return Ok(await _lessonRepository.UpdateLessonByUser(iUpdate));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}
