using Math4FunBackedn.Entities;

namespace Math4FunBackedn.Repositories.StreakRepo
{
    public class StreakUpdateReponse
    {
        public Streak? Streak { get; set; }
        public bool IsContinueStreakUpdate { get; set; }
    }
    public interface IStreakRepository
    {
        Task<StreakUpdateReponse> UpdateStreak(DateTime dateCompleteLesson, Guid UserId);
        Task<StreakUpdateReponse> GetCurrentStreak(Guid UserId);
    }
}
