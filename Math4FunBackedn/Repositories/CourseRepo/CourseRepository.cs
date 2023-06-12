using Math4FunBackedn.DBContext;
using Math4FunBackedn.DTO;
using Math4FunBackedn.Entities;
using Microsoft.EntityFrameworkCore;

namespace Math4FunBackedn.Repositories.CourseRepo
{
    public class CourseRepository : ICourseRepository
    {
        private readonly MyDbContext _context;
        public CourseRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<Course> AddCourse(AddCourseDTO iCourse)
        {
            var newCourse = new Course()
            {
                Id = Guid.NewGuid(),
                Description = iCourse.Description,
                Name = iCourse.Name,
                Image = iCourse.Image
            };
            var result = await _context.Course.AddAsync(newCourse);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<Course>> GetAllCourse()
        {
            var list = new List<Course>();
            list = _context.Course.ToList<Course>();
            return list;
        }

        public async Task<List<Course>> GetCourseByUserId(Guid iUserId)
        {
            var listUserCourse = await _context.Users_Courses.Include(uc => uc.Course).Where(uc => uc.UserId == iUserId).ToListAsync();
            var list = new List<Course>();
            if (list == null)
            {
                throw new Exception("Người dùng chưa đăng ký khóa học nào");
            }
            listUserCourse.ForEach(uc => {
                list.Add(uc.Course);
            });
            return list;
        }

        public async Task<Course> GetDetailCourse(Guid userId, Guid courseId)
        {
            var user = await _context.User.Include(u => u.Users_Courses).ThenInclude(uc => uc.Course).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("Không tồn tại người dùng này");
            }
            var userCourse = user.Users_Courses.FirstOrDefault(uc => uc.CourseId == courseId);
            var course = userCourse.Course;
            return course;
        }
        public async Task<int> RegisterCourse(RegisterCourseDTO iRegister)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == iRegister.UserId);
            var course = await _context.Course.FirstOrDefaultAsync(c => c.Id == iRegister.CourseId);
            if (user == null || course == null)
            {
                throw new Exception("Không tìm thấy người dùng hoặc khóa học");
            }
            var listUC = await _context.Users_Courses.Where(uc => uc.UserId == iRegister.UserId&& uc.CourseId==iRegister.CourseId).ToListAsync();
            if (!listUC.Any()) throw new Exception("Người dùng đã đăng ký khóa học");
            if(course.TotalMember == null)
            {
                course.TotalMember = 0;
            }
            course.TotalMember = course.TotalMember + 1;
            var user_course = new Users_Courses()
            {
                Id = Guid.NewGuid(),
                User = user,
                Course = course,
                UserId = user.Id,
                CourseId = course.Id
            };
            await _context.Users_Courses.AddAsync(user_course);
            await _context.SaveChangesAsync();
            return 1;
        }

        public Task<int> UpdateCourse(Guid courseId)
        {
            throw new NotImplementedException();
        }
    }
}
