using Math4FunBackedn.DBContext;
using Math4FunBackedn.DTO;
using Math4FunBackedn.Entities;
using Math4FunBackedn.Repositories.StreakRepo;
using Microsoft.EntityFrameworkCore;

namespace Math4FunBackedn.Repositories.LessonRepo
{
    public class LessonRepository : ILessonRepository
    {
        private readonly MyDbContext _context;
        private readonly IStreakRepository _streakRepository;
        public LessonRepository(MyDbContext context, IStreakRepository streakRepo)
        {
            _context = context;
            _streakRepository = streakRepo;

        }
        public async Task<int> AddLesson(AddLessonDTO iAdd)
        {
            var chapter = await _context.Chapter.Include(c => c.LessonList).FirstOrDefaultAsync(c => c.Id == iAdd.ChapterId);
            if (chapter == null) throw new Exception("Không tìm thấy chương học");
            var newLesson = new Lesson()
            {
                Id = Guid.NewGuid(),
                Title = iAdd.Title,
                ExpGained = iAdd.ExpGained,
                ChapterId = chapter.Id,
                Index = chapter.LessonList.Count,
                CreatedDate = DateTime.Now
            };
            chapter.LessonList!.Append(newLesson);
            await _context.Lesson.AddAsync(newLesson);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> DeleteLesson(Guid lessonId)
        {
            var lesson = await _context.Lesson.FirstOrDefaultAsync(l => l.Id == lessonId);
            if(lesson == null) throw new Exception("Không tìm thấy chương học");
            _context.Lesson.Remove(lesson);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<Lesson> Detail(Guid id)
        {
            var lesson = await _context.Lesson.Include(l => l.QuestionList).FirstOrDefaultAsync(l => l.Id == id);
            if (lesson == null) throw new Exception("Không tìm thấy chương học");
            lesson.QuestionList = lesson.QuestionList.OrderBy(q => q.createdAt).ToList();
            return lesson;
        }

        public async  Task<int> Update(UpdateLessonDTO iUpdate)
        {
            var lesson = await _context.Lesson.FirstOrDefaultAsync(l => l.Id == iUpdate.Id);
            if (lesson == null) throw new Exception("Không tìm thấy chương học");
            lesson.Title = iUpdate.Title;
            lesson.ExpGained = iUpdate.ExpGained;
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<StreakUpdateReponse> UpdateLessonByUser(UserUpdateLessonDTO iUpdate)
        {
            var user = await _context.User.Include(u => u.Users_Courses).ThenInclude(uc => uc.Course).ThenInclude(c => c.ChapterList).ThenInclude(chapter => chapter.LessonList).FirstOrDefaultAsync(u => u.Id == iUpdate.UserId);
            var userCourse = user.Users_Courses.FirstOrDefault(uc => uc.CourseId == iUpdate.CourseId);
            var course = userCourse.Course;
            var chapter = course.ChapterList.FirstOrDefault(chapter => chapter.Id == iUpdate.ChapterId);
            var lesson = chapter.LessonList.FirstOrDefault(lesson => lesson.Id == iUpdate.LessonId);
            if (lesson == null)
            {
                throw new Exception("Không tìm thấy bài học");
            }
            if (iUpdate.Status == true)
            {
                if (user.TotalExp == null)
                {
                    user.TotalExp = 0;
                }
            }
            int? expPlus = 0;
            if(lesson.Status == true)
            {
                expPlus = 5;
            }
            else
            {
                expPlus = lesson.ExpGained;
            }
            lesson.Status = iUpdate.Status;
            user.TotalExp += expPlus;
            // If complete lesson => update streak
            var responseUpdateStreak = await _streakRepository.UpdateStreak(DateTime.Now, iUpdate.UserId);
            await _context.SaveChangesAsync();
            return responseUpdateStreak;
        }
    }
}
