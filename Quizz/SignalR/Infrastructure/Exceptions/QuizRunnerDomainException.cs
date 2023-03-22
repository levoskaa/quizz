﻿using Quizz.Common.ErrorHandling;

namespace Quizz.SignalR.Infrastructure.Exceptions;

public class QuizRunnerDomainException : QuizzException
{
    public QuizRunnerDomainException()
    { }

    public QuizRunnerDomainException(string message)
        : base(message)
    { }

    public QuizRunnerDomainException(string message, params string[] errorCodes)
        : base(message, errorCodes)
    { }
}
