using Quizz.Common.ErrorHandling;

namespace Quizz.GameService.Infrastructure.Exceptions
{
    public class GameServiceDomainException : QuizzException
    {
        public GameServiceDomainException()
        { }

        public GameServiceDomainException(string message)
            : base(message)
        { }

        public GameServiceDomainException(string message, params string[] errorCodes)
            : base(message, errorCodes)
        { }
    }
}