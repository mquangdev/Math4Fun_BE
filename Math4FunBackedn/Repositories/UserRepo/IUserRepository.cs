using Math4FunBackedn.DTO;
using Math4FunBackedn.Entities;
namespace Math4FunBackedn.Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<User> GetById(Guid id);
        Task<PageResult<User>> GetAll(int page, int limit, string keyword);
        Task<int> UpdateUser(UpdateUserDTO iUpdate);
    }
}
