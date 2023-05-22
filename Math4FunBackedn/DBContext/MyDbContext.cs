using Math4FunBackedn.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Math4FunBackedn.DBContext
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        #region DBset
        public DbSet<Account> Account { get; set; }
        public DbSet<User> User { get; set; }
        #endregion
    }
}
