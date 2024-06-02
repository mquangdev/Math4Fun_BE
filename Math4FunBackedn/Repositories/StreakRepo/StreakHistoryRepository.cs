using Math4FunBackedn.DBContext;
using Math4FunBackedn.Entities;

namespace Math4FunBackedn.Repositories.StreakRepo
{
    public class StreakHistoryRepository : IStreakHistoryRepository
    {
        public readonly MyDbContext _context;
        public StreakHistoryRepository(MyDbContext context)
        {
            _context = context;
        }

        public Task<StreakHistory[]> GetStreakHistory(DateTime StartDate, DateTime EndDate, Guid UserId)
        {
            var listUserHistory = _context.StreakHistory.Where(s => s.UserId == UserId).Where(s => s.EndDate >= StartDate || s.StartDate <= EndDate).ToList();
            if(listUserHistory == null)
            {
                return Task.FromResult(Array.Empty<StreakHistory>());
            }
            return Task.FromResult(listUserHistory.ToArray()); ;
        }
    }
}
