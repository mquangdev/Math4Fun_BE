using Math4FunBackedn.DBContext;
using Math4FunBackedn.Entities;

namespace Math4FunBackedn.Repositories.StreakRepo
{
    public class StreakRepository : IStreakRepository
    {
        private readonly MyDbContext _context;
        public StreakRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<Streak> GetCurrentStreak(Guid UserId)
        {
            var streak = _context.Streak.FirstOrDefault(s => s.UserId == UserId);
            if(streak == null)
            {
                throw new Exception("Người dùng chưa có streak");
            }
            return streak;
        }

        public async Task<Streak> UpdateStreak(DateTime dateCompleteLesson, Guid UserId)
        {
            var streakUser = _context.Streak.FirstOrDefault(s => s.UserId == UserId);
            // If does not exist any streak of this user -> create new record
            if(streakUser == null)
            {
                var streak = new Streak
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    CurrentStreakCount = 1,
                    LastLessonDate = dateCompleteLesson,
                    StartLessonDate = dateCompleteLesson
                };
                _context.Streak.Add(streak);
                await _context.SaveChangesAsync();
                return streak;
            }
            // If already exist streak record of this user
            else
            {
                // already complete lesson today
                if(streakUser.LastLessonDate.Date == dateCompleteLesson.Date)
                {
                    return streakUser;
                }
                // finish the lesson yesterday
                else if(streakUser.LastLessonDate.Date == dateCompleteLesson.AddDays(-1).Date)
                {
                    streakUser.CurrentStreakCount++;
                    streakUser.LastLessonDate = dateCompleteLesson;
                    await _context.SaveChangesAsync();
                    return streakUser;
                }
                else
                {
                    // Miss a day -> create new streak history record and reset streak
                    var newStreakHistory = new StreakHistory {
                        Id = Guid.NewGuid(),
                        EndDate = streakUser.LastLessonDate,
                        StartDate = streakUser.LastLessonDate.AddDays(1 - streakUser.CurrentStreakCount),
                        StreakLength = streakUser.CurrentStreakCount
                    };
                    _context.StreakHistory.Add(newStreakHistory);
                    // reset streak and reset startDate
                    streakUser.CurrentStreakCount = 1;
                    streakUser.LastLessonDate = dateCompleteLesson;
                    streakUser.StartLessonDate = dateCompleteLesson;
                    await _context.SaveChangesAsync();
                    return streakUser;
                }
                
            }
           
        }
    }
}

