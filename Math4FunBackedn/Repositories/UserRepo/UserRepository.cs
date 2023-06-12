using Math4FunBackedn.DBContext;
using Math4FunBackedn.Entities;
using Microsoft.EntityFrameworkCore;

namespace Math4FunBackedn.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;
        public UserRepository(MyDbContext iContext)
        {
            _context = iContext;
        }

        public async Task<List<User>> GetAll()
        {
            var list = new List<User>();
            list = _context.User.ToList<User>();
            return list;
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _context.User.FirstOrDefaultAsync(e => e.Id == id);
            if(user == null)
            {
                throw new Exception("User không tồn tại");
            }
            return user;
        }
    }
}
