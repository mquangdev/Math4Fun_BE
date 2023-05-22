using Math4FunBackedn.DTO;
using Math4FunBackedn.Entities;

namespace Math4FunBackedn.Repositories.AccountRepo
{
    public interface IAccountRepository
    {
        Task<int> Create(CreateAccountDTO iAcc);
        Task<User> SignIn(SignInDTO iAcc);
    }
}
