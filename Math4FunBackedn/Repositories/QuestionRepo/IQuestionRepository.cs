using Math4FunBackedn.DTO;
using Math4FunBackedn.Entities;

namespace Math4FunBackedn.Repositories.QuestionRepo
{
    public interface IQuestionRepository
    {
        Task<int> Add(AddQuestionDTO iAdd);
        Task<Question> DetailQuestion(Guid questionId);
        Task<int> Remove(Guid questionId);
        Task<int> Update(UpdateQuestionDTO iUpdate);
    }
}
