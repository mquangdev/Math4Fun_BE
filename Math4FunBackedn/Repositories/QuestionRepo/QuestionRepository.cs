using Math4FunBackedn.DBContext;
using Math4FunBackedn.DTO;
using Math4FunBackedn.Entities;
using Microsoft.EntityFrameworkCore;

namespace Math4FunBackedn.Repositories.QuestionRepo
{
    public enum QuestionType
    {
        choosePair,
        chooseAnswer,
        chooseToBlank,
        typeToBlank
    };
    public class QuestionRepository : IQuestionRepository
    {

        private readonly MyDbContext _context;
        public QuestionRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(AddQuestionDTO iAdd)
        {
            switch (iAdd.Type)
            {
                case QuestionType.chooseAnswer:
                    {
                        var id = Guid.NewGuid();
                        var newQuestion = new Question()
                        {
                            Id = id,
                            Image = iAdd.Image,
                            Text = iAdd.Text,
                            Type = iAdd.Type,
                            Value = iAdd.Value,
                            LessonId = iAdd.LessonId
                        };
                        var listAnswer = new List<Answer>();
                        iAdd.AnswerList.ForEach((answer) =>
                        {
                            var newAnswer = new Answer() {
                                Id = Guid.NewGuid(),
                                Text = answer.Text,
                                Value = answer.Value,
                                QuestionId = id
                            };
                            listAnswer.Add(newAnswer);
                        });
                        newQuestion.AnswerList = listAnswer;
                        await _context.Question.AddAsync(newQuestion);
                        await _context.SaveChangesAsync();
                        break;
                    }
                case QuestionType.choosePair:
                    {
                        var id = Guid.NewGuid();
                        var newQuestion = new Question()
                        {
                            Id = id,
                            Image = iAdd.Image,
                            Text = iAdd.Text,
                            Type = iAdd.Type,
                            LessonId = iAdd.LessonId
                        };
                        var listAnswer = new List<Answer>();
                        iAdd.AnswerList.ForEach((answer) =>
                        {
                            var newAnswer = new Answer()
                            {
                                Id = Guid.NewGuid(),
                                Text = answer.Text,
                                Value = answer.Value,
                                QuestionId = id
                            };
                            listAnswer.Add(newAnswer);
                        });
                        newQuestion.AnswerList = listAnswer;
                        await _context.Question.AddAsync(newQuestion);
                        await _context.SaveChangesAsync();
                        break;
                    }
            }
            return 1;
        }
        public async Task<Question> DetailQuestion(Guid questionId)
        {
            var question = await _context.Question.Include(q => q.AnswerList).FirstOrDefaultAsync(q => q.Id == questionId);
            return question;
        }
        public async Task<int> Remove(Guid questionId)
        {
            var question = await _context.Question.FirstOrDefaultAsync(q => q.Id == questionId);
            _context.Question.Remove(question);
            await _context.SaveChangesAsync();
            return 1;
        }
    }
}
