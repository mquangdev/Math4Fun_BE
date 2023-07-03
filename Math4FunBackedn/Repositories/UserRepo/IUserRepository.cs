using Math4FunBackedn.DTO;
using Math4FunBackedn.Entities;

namespace Math4FunBackedn.Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<User> GetById(Guid id);
        Task<List<User>> GetAll();
        Task<int> UpdateUser(UpdateUserDTO iUpdate);
    }
}
