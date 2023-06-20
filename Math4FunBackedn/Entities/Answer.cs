using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Math4FunBackedn.Entities
{
    public class Answer
    {
        [Key]
        public Guid Id { get; set; }
        public string? Text { get; set; }
        public string? Value { get; set; }
       // public bool? IsCorrect { get; set; }
        [ForeignKey("QuestionId")]
        public Guid QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
