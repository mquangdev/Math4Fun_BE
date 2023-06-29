using Math4FunBackedn.Entities;
using Math4FunBackedn.Repositories.QuestionRepo;
using System.ComponentModel.DataAnnotations.Schema;

namespace Math4FunBackedn.DTO
{
    public class AddQuestionDTO
    {
        public string? Text { get; set; }
        public string? Image { get; set; }
        public QuestionType? Type { get; set; }
        public string? Value { get; set; }
        public Guid LessonId { get; set; }
        public List<AddAnswer>? AnswerList { get; set; }
    }
}
