using Math4FunBackedn.Repositories.QuestionRepo;

namespace Math4FunBackedn.DTO
{
    public class UpdateQuestionDTO
    {
        public Guid Id { get; set; }
        public string? Text { get; set; }
        public string? TextBonus { get; set; }
        public string? Image { get; set; }
        public QuestionType? Type { get; set; }
        public string? Value { get; set; }
        public List<AddAnswer>? AnswerList { get; set; }
    }
}
