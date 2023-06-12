using Math4FunBackedn.Entities;

namespace Math4FunBackedn.Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<User> GetById(Guid id);
        Task<List<User>> GetAll();
    }
}
