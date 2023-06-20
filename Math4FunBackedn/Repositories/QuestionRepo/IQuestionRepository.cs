using Math4FunBackedn.DTO;

namespace Math4FunBackedn.Repositories.QuestionRepo
{
    public interface IQuestionRepository
    {
        Task<int> Add(AddQuestionDTO iAdd);
    }
}
