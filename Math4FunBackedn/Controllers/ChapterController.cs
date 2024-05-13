using Math4FunBackedn.DTO;
using Math4FunBackedn.Repositories.ChapterRepo;
using Math4FunBackedn.Repositories.CourseRepo;
using Microsoft.AspNetCore.Mvc;

namespace Math4FunBackedn.Controllers
{
    [Route("Chapter")]
    public class ChapterController : Controller
    {
        private IChapterRepository _chapterRepository;
        public ChapterController(IChapterRepository chapterRepository)
        {
            _chapterRepository = chapterRepository;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddChapter([FromBody] AddChapterDTO iChapter)
        {
            try
            {
                return Ok(await _chapterRepository.AddChapter(iChapter));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid chapterId)
        {
            try
            {
                return Ok(await _chapterRepository.Delete(chapterId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpGet("Detail")]
        public async Task<IActionResult> Detail(Guid chapterId)
        {
            try
            {
                return Ok(await _chapterRepository.Detail(chapterId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateChapterDTO iUpdate)
        {
            try
            {
                return Ok(await _chapterRepository.Update(iUpdate));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
