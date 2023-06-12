using Math4FunBackedn.DTO;
using Math4FunBackedn.Repositories.AccountRepo;
using Microsoft.AspNetCore.Mvc;

namespace Math4FunBackedn.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _AccountRepo;
        public AccountController(IAccountRepository iAccountRepo)
        {
            _AccountRepo = iAccountRepo;
        }
        [HttpPost("Auth/Create")]
        public async Task<IActionResult> Create([FromBody] CreateAccountDTO iNewAcc)
        {
            try
            {
                return Ok(await _AccountRepo.Create(iNewAcc));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPost("Auth/Login")]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO iAcc)
        {
            try
            {
                return Ok(await _AccountRepo.SignIn(iAcc));
;            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
