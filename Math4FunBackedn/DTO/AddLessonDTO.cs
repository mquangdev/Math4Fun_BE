using System.ComponentModel.DataAnnotations.Schema;

namespace Math4FunBackedn.DTO
{
    public class AddLessonDTO
    {
        public string? Title { get; set; }
        public int? ExpGained { get; set; }
        public Guid ChapterId { get; set; }
    }
}
