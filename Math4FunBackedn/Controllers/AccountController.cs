using Math4FunBackedn.DTO;
using Math4FunBackedn.Repositories.AccountRepo;
using Math4FunBackedn.Repositories.MailRepo;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace Math4FunBackedn.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _AccountRepo;
        private readonly IMailRepository _mailRepository;
        public AccountController(IAccountRepository iAccountRepo,IMailRepository mailRepository)
        {
            _AccountRepo = iAccountRepo;
            _mailRepository = mailRepository;
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
        [HttpPost("Auth/ForgotPw")]
        public async Task<IActionResult> ForgotPw(string email)
        {
            try
            {
                await _mailRepository.SendPasswordResetMail(email);
                return Ok(1);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("Auth/CheckOTP")]
        public async Task<IActionResult> CheckOTP(string otp, string email)
        {
            try
            {
                return Ok(await _mailRepository.CheckOTP(email, otp));  
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("Auth/ChangePw")]
        public async Task<IActionResult> ChangePw([FromBody] ChangePwDTO iChange)
        {
            try
            {
                return Ok(await _AccountRepo.ChangePw(iChange));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
