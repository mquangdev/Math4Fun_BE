using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Math4FunBackedn.Entities
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }
        public string? Text { get; set; }
        public string? Image { get; set; }
        public int? Type { get; set; }
        [ForeignKey("LessonId")]
        public Guid LessonId { get; set; }
        public Lesson? Lesson { get; set; }
        public ICollection<Answer>? AnswerList { get; set; }
    }
}
