using System;
using System.Collections.Generic;

namespace Quizz.Common.ErrorHandling
{
    public abstract class QuizzException : Exception
    {
        public IEnumerable<string> ErrorCodes { get; set; }

        public QuizzException()
        { }

        public QuizzException(string message)
            : base(message)
        { }

        public QuizzException(string message, params string[] errorCodes)
            : base(message)
        {
            ErrorCodes = errorCodes;
        }
    }
}