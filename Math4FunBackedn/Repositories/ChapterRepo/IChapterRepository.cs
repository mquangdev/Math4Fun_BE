using Math4FunBackedn.DTO;
using Math4FunBackedn.Entities;

namespace Math4FunBackedn.Repositories.ChapterRepo
{
    public interface IChapterRepository
    {
        Task<int> AddChapter(AddChapterDTO iChapter);
        Task<int> Delete(Guid chapterId);
        Task<Chapter> Detail(Guid chapterId);
        Task<int> Update(UpdateChapterDTO iUpdate);
    }
}
