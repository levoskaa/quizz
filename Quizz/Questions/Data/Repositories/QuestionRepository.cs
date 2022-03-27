using Quizz.Common.DataAccess;

namespace Quizz.Questions.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly QuestionsContext context;

        public IUnitOfWork UnitOfWork => context;

        public QuestionRepository(QuestionsContext context)
        {
            this.context = context;
        }
    }
}
