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
    }
}
