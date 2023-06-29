namespace Math4FunBackedn.DTO
{
    public class UserUpdateLessonDTO
    {
        public Guid CourseId { get; set; }
        public Guid ChapterId { get; set; }
        public Guid LessonId { get; set; }
        public Guid UserId { get; set; }
        public bool? Status { get; set; }
    }
}
