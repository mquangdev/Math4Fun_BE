using Math4FunBackedn.DBContext;
using Math4FunBackedn.DTO;
using Math4FunBackedn.Entities;
using Microsoft.EntityFrameworkCore;

namespace Math4FunBackedn.Repositories.AccountRepo
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyDbContext _context;
        public AccountRepository(MyDbContext iContext)
        {
            _context = iContext;
        }

        public async Task<int> ChangePw(ChangePwDTO iChangePw)
        {
            var acc = await _context.Account.FirstOrDefaultAsync(a => a.Email == iChangePw.email);
            if(acc == null) throw new Exception("Tài khoản không đúng");
            acc.Password = iChangePw.password;
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> Create(CreateAccountDTO iAcc)
        {
            var checkAcc = await _context.Account.FirstOrDefaultAsync(e => e.Username == iAcc.Username);
            if(checkAcc != null)
            {
                throw new Exception("Tên đăng nhập đã tồn tại");
            }
            Guid newId = Guid.NewGuid();
            var newAcc = new Account()
            {
                Id = newId,
                Username = iAcc.Username,
                Password = iAcc.Password,
                Email = iAcc.Email
            };
            var newUser = new User()
            {
                Id = newId,
                Username = iAcc.Username,
                Dob = iAcc.Dob,
                Fullname = iAcc.Fullname,
                Email = iAcc.Email,
                Role = 0
            };
            await _context.Account.AddAsync(newAcc);
            await _context.User.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return 1;
        }
        public async Task<User> SignIn(SignInDTO iAcc)
        {
            var acc = await _context.Account.FirstOrDefaultAsync(e => e.Username == iAcc.username);
            if(acc == null || acc.Password != iAcc.password)
            {
                throw new Exception("Sai tài khoản hoặc mật khẩu");
            }
            var user = await _context.User.SingleOrDefaultAsync(e => e.Id == acc.Id);
            return user;
        }
    }
}
