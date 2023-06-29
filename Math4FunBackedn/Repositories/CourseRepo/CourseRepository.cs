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
                Image = iCourse.Image,
            };
            var result = await _context.Course.AddAsync(newCourse);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<int> Delete(Guid courseId)
        {

            var course = await _context.Course.FirstOrDefaultAsync(c => c.Id == courseId);
            if (course == null)
            {
                throw new Exception("Không tìm thấy khóa học");
            }
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<Course> DetailCourse(Guid courseId)
        {
            var course = await _context.Course.Include(c => c.ChapterList).FirstOrDefaultAsync(c => c.Id == courseId);
            if (course == null)
            {
                throw new Exception("Không tìm thấy khóa học");
            }
            course.ChapterList.OrderBy(c => c.CreatedDate);
            return course;
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
            if (listUserCourse.Count == 0)
            {
                throw new Exception("Người dùng chưa đăng ký khóa học nào");
            }
            listUserCourse.ForEach(uc =>
            {
                list.Add(uc.Course);
            });
            return list;
        }

        public async Task<Course> GetDetailCourseByUserId(Guid userId, Guid courseId)
        {
            var user = await _context.User.Include(u => u.Users_Courses).ThenInclude(uc => uc.Course).ThenInclude(c => c.ChapterList).ThenInclude(chapter => chapter.LessonList).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("Không tồn tại người dùng này");
            }
            var userCourse = user.Users_Courses.FirstOrDefault(uc => uc.CourseId == courseId);
            var course = userCourse.Course;
            course.ChapterList = course.ChapterList.OrderBy(chapter => chapter.CreatedDate).ToList();
            foreach (var chapter in course.ChapterList)
            {
                chapter.LessonList = chapter.LessonList.OrderBy(l => l.Index).ToList();
            }
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
            var listUC = await _context.Users_Courses.Where(uc => uc.UserId == iRegister.UserId && uc.CourseId == iRegister.CourseId).ToListAsync();
            if (listUC.Count > 0) throw new Exception("Người dùng đã đăng ký khóa học");
            if (course.TotalMember == null)
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

        public async Task<int> UpdateCourse(UpdateCourseDTO iUpdate)
        {
            var course = await _context.Course.FirstOrDefaultAsync(c => c.Id == iUpdate.Id);
            if (course == null)
            {
                throw new Exception("Không tìm thấy khóa học");
            }
            course.Description = iUpdate.Description;
            course.Name = iUpdate.Name;
            course.Image = iUpdate.Image;
            await _context.SaveChangesAsync();
            return 1;
        }

    }
}
