using Math4FunBackedn.DTO;
using Math4FunBackedn.Repositories.AccountRepo;
using Math4FunBackedn.Repositories.UserRepo;
using Microsoft.AspNetCore.Mvc;

namespace Math4FunBackedn.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _UserRepo;
        public UserController(IUserRepository iUserRepo)
        {
            _UserRepo = iUserRepo;
        }
        [HttpGet("User/GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _UserRepo.GetById(id));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               ex.Message);
            } 
        }
        [HttpGet("User/GetAll")]
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
