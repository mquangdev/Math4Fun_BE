using Math4FunBackedn.Entities;

namespace Math4FunBackedn.Repositories.StreakRepo
{
    public interface IStreakHistoryRepository
    {
        Task<StreakHistory[]> GetStreakHistory(DateTime StartDate, DateTime EndDate, Guid UserId);
    }
}
