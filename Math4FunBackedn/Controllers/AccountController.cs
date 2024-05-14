using Math4FunBackedn.DTO;
using Math4FunBackedn.Repositories.AccountRepo;
using Math4FunBackedn.Repositories.MailRepo;
using Math4FunBackedn.Repositories.TokenRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Math4FunBackedn.Controllers
{
    [Route("Auth")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _AccountRepo;
        private readonly IMailRepository _mailRepository;
        private readonly IConfiguration _config;
        private readonly ITokenRepository _tokenRepo;
        public AccountController(IAccountRepository iAccountRepo,IMailRepository mailRepository, IConfiguration iConfig, ITokenRepository iToken)
        {
            _AccountRepo = iAccountRepo;
            _mailRepository = mailRepository;
            _config = iConfig;
            _tokenRepo = iToken;
        }
        [HttpPost("Create")]
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
        [HttpPost("Login")]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO iAcc)
        {
            try
            {
                var user = await _AccountRepo.SignIn(iAcc);
                if(user == null)
                {
                    return Unauthorized();
                }
                return Ok(
                    new
                    {
                        user = user,
                        token = _tokenRepo.GenerateToken(user).Result
                    });
;           }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("ForgotPw")]
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
        [HttpPost("CheckOTP")]
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
        [HttpPost("ChangePw")]
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
