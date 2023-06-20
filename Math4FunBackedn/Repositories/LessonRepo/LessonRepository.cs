using Math4FunBackedn.DBContext;
using Math4FunBackedn.DTO;
using Math4FunBackedn.Entities;
using Microsoft.EntityFrameworkCore;

namespace Math4FunBackedn.Repositories.LessonRepo
{
    public class LessonRepository : ILessonRepository
    {
        private readonly MyDbContext _context;
        public LessonRepository(MyDbContext context)
        {
            _context = context;
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
    }
}
