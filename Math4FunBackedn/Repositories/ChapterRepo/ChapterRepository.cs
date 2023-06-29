using Math4FunBackedn.DBContext;
using Math4FunBackedn.DTO;
using Math4FunBackedn.Entities;
using Microsoft.EntityFrameworkCore;
using static MailKit.Net.Imap.ImapMailboxFilter;

namespace Math4FunBackedn.Repositories.ChapterRepo
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly MyDbContext _context;
        public ChapterRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddChapter(AddChapterDTO iChapter)
        {
            var course = await _context.Course.Include(c => c.ChapterList).FirstOrDefaultAsync(c => c.Id == iChapter.CourseId);
            if (course == null) throw new Exception("Không tìm thấy khóa học");
            var newChapter = new Chapter()
            {
                Id = Guid.NewGuid(),
                Title = iChapter.Title,
                Index = course.ChapterList != null ? course.ChapterList.Count + 1 : 1,
                SubTile = iChapter.SubTile,
                Instruction = iChapter.Instruction,
                CourseId = iChapter.CourseId,
                CreatedDate = DateTime.Now
            };
            course.ChapterList!.Append(newChapter);
            await _context.Chapter.AddAsync(newChapter);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> Delete(Guid chapterId)
        {
            var chapter = await _context.Chapter.FirstOrDefaultAsync(c => c.Id == chapterId);
            if (chapter == null) throw new Exception("Không tìm thấy chương học này");
            _context.Chapter.Remove(chapter);
            await _context.SaveChangesAsync();  
            return 1;
        }

        public async Task<Chapter> Detail(Guid chapterId)
        {
            var chapter = await _context.Chapter.Include(c => c.LessonList).FirstOrDefaultAsync(c => c.Id == chapterId);
            chapter.LessonList = chapter.LessonList.OrderBy(l => l.Index).ToList();
            if (chapter == null) throw new Exception("Không tìm thấy chương học này");
            return chapter;
        }

        public async Task<int> Update(UpdateChapterDTO iUpdate)
        {
            var chapter = await _context.Chapter.FirstOrDefaultAsync(c => c.Id == iUpdate.Id);
            if (chapter == null) throw new Exception("Không tìm thấy chương học này");
            chapter.Title = iUpdate.Title;
            chapter.SubTile = iUpdate.SubTile;
            chapter.Instruction = iUpdate.Instruction;
            await _context.SaveChangesAsync();
            return 1;
        }
    }
}
