using Math4FunBackedn.DTO;
using Math4FunBackedn.Entities;

namespace Math4FunBackedn.Repositories.CourseRepo
{
    public interface ICourseRepository
    {
        Task<Course> AddCourse(AddCourseDTO iCourse);
        Task<List<Course>> GetAllCourse();
        Task<int> RegisterCourse(RegisterCourseDTO iRegister);
        Task<List<Course>> GetCourseByUserId(Guid iUserId);
        Task<Course> GetDetailCourse(Guid userId, Guid courseId);
        Task<int> UpdateCourse(Guid courseId);

    }
}
