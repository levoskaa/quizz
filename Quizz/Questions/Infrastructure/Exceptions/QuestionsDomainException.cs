using Quizz.Common.ErrorHandling;

namespace Quizz.Questions.Infrastructure.Exceptions
{
    public class QuestionsDomainException : QuizzException
    {
        public QuestionsDomainException()
        { }

        public QuestionsDomainException(string message)
            : base(message)
        { }

        public QuestionsDomainException(string message, params string[] errorCodes)
            : base(message, errorCodes)
        { }
    }
}