using Math4FunBackedn.Entities;

namespace Math4FunBackedn.Repositories.StreakRepo
{
    public interface IStreakRepository
    {
        Task<Streak> UpdateStreak(DateTime dateCompleteLesson, Guid UserId);
        Task<Streak> GetCurrentStreak(Guid UserId);
    }
}
