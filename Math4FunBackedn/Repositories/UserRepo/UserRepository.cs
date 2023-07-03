using Math4FunBackedn.DBContext;
using Math4FunBackedn.DTO;
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
            if (user == null)
            {
                throw new Exception("User không tồn tại");
            }
            return user;
        }

        public async Task<int> UpdateUser(UpdateUserDTO iUpdate)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == iUpdate.Id);
            if (user == null)
            {
                throw new Exception("User không tồn tại");
            }
            user.Username = iUpdate.Username != null ? iUpdate.Username : user.Username;
            user.Fullname = iUpdate.Fullname != null ? iUpdate.Fullname : user.Fullname;
            user.Gender = iUpdate.Gender != null ? iUpdate.Gender : user.Gender;
            user.Dob = iUpdate.Dob != null ? iUpdate.Dob : user.Dob;
            user.Avatar = iUpdate.Avatar != null ? iUpdate.Avatar : user.Avatar;
            user.Email = iUpdate.Email != null ? iUpdate.Email : user.Email;
            user.TotalExp = iUpdate.TotalExp != null ? iUpdate.TotalExp : user.TotalExp;
            user.TotalGem = iUpdate.TotalGem != null ? iUpdate.TotalGem : user.TotalGem;
            user.Role = iUpdate.Role != null ? iUpdate.Role : user.Role;
            await _context.SaveChangesAsync();
            return 1;
        }
    }
}
