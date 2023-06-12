using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Math4FunBackedn.Entities
{
    public class Chapter
    {
        [Key]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? SubTile { get; set; }
        public string? Instruction { get; set; }
        public bool? Status { get; set; }
        [ForeignKey("CourseId")]
        public Guid CourseId { get; set; }
        public Course? Course { get; set; }
        public ICollection<Lesson>? LessonList { get; set; }
    }
}
