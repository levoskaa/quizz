using System;

namespace Quizz.Common.ErrorHandling;

public class ForbiddenException : Exception
{
    public ForbiddenException()
    { }

    public ForbiddenException(string mesage)
        : base(mesage)
    { }
}
