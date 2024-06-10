using Math4FunBackedn.DTO;
using Math4FunBackedn.Entities;
using Math4FunBackedn.Repositories.StreakRepo;

namespace Math4FunBackedn.Repositories.LessonRepo
{
    public interface ILessonRepository
    {
        Task<int> AddLesson(AddLessonDTO iAdd);
        Task<int> DeleteLesson(Guid lessonId);
        Task<int> Update(UpdateLessonDTO iUpdate);
        Task<Lesson> Detail(Guid id);
        Task<StreakUpdateReponse> UpdateLessonByUser(UserUpdateLessonDTO iUpdate);
    }
}
