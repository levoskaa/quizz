using Quizz.Common.ErrorHandling;

namespace Quizz.Common.ErroHandling;

public class EntityNotFoundException : QuizzException
{
    public EntityNotFoundException()
    { }

    public EntityNotFoundException(string message)
        : base(message)
    { }

    public EntityNotFoundException(string message, params string[] errorCodes)
        : base(message, errorCodes)
    { }
}
