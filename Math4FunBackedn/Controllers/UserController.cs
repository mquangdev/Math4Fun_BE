using Math4FunBackedn.DTO;
using Math4FunBackedn.Repositories.AccountRepo;
using Math4FunBackedn.Repositories.TokenRepo;
using Math4FunBackedn.Repositories.UserRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Math4FunBackedn.Controllers
{
    [Authorize]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IUserRepository _UserRepo;
        private ITokenRepository _tokenRepository;
        public UserController(IUserRepository iUserRepo, ITokenRepository tokenRepository)
        {
            _UserRepo = iUserRepo;
            _tokenRepository = tokenRepository;
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById()
        {
            string authorizationHeader = Request.Headers["Authorization"];
            try
            {
                Guid userId = await _tokenRepository.DecodeToken(authorizationHeader);
                return Ok(await _UserRepo.GetById(userId));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               ex.Message);
            } 
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _UserRepo.GetAll());
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               ex);
            }
        }
        [HttpPost("User/Update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO iUpdate)
        {
            try
            {
                return Ok(await _UserRepo.UpdateUser(iUpdate));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               ex);
            }
        }
    }
}
