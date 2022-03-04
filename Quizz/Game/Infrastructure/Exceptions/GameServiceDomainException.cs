using System;
using System.Collections.Generic;

namespace Quizz.GameService.Infrastructure.Exceptions
{
    public class GameServiceDomainException : Exception
    {
        public IEnumerable<string> ErrorCodes { get; set; }

        public GameServiceDomainException()
        { }

        public GameServiceDomainException(string message)
            : base(message)
        { }

        public GameServiceDomainException(string message, IEnumerable<string> errorCodes)
            : base(message)
        {
            ErrorCodes = errorCodes;
        }
    }
}