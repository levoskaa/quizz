using Quizz.Common.ErrorHandling;

namespace Quizz.SignalR.Infrastructure.Exceptions
{
    public class NoQuestionsRemainingException : QuizzException
    {
        public NoQuestionsRemainingException()
        { }

        public NoQuestionsRemainingException(string message)
            : base(message)
        { }

        public NoQuestionsRemainingException(string message, params string[] errorCodes)
            : base(message, errorCodes)
        { }
    }
}