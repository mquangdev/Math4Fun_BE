using Math4FunBackedn.Entities;
using Math4FunBackedn.Repositories.StreakRepo;
using Math4FunBackedn.Repositories.TokenRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Math4FunBackedn.Controllers
{
    [Authorize]
    [Route("Streak")]
    public class StreakController : ControllerBase
    {
        private readonly IStreakRepository _streakRepo;
        private readonly IStreakHistoryRepository _streakHistoryRepo;
        private readonly ITokenRepository _tokenRepo;
        public StreakController(IStreakHistoryRepository streakHistoryRepo, IStreakRepository streakRepo, ITokenRepository tokenRepo)
        {
            _streakRepo = streakRepo;
            _streakHistoryRepo = streakHistoryRepo;
            _tokenRepo = tokenRepo;
        }
        [HttpGet("CurrentStreak")]
        public async Task<IActionResult> GetCurrentStreak()
        {
            string authorizationHeader = Request.Headers["Authorization"];
            try
            {
                var userId = await _tokenRepo.DecodeToken(authorizationHeader);
                return Ok(await _streakRepo.GetCurrentStreak(userId));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetStreakHistory")]
        public async Task<IActionResult> GetStreakHistory(DateTime startDate, DateTime endDate)
        {
            string authorizationHeader = Request.Headers["Authorization"];
            try
            {
                var userId = await _tokenRepo.DecodeToken(authorizationHeader);
                return Ok(await _streakHistoryRepo.GetStreakHistory(startDate, endDate, userId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
