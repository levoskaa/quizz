namespace Quizz.Common.Models;

public class TypeInAnswerQuestion : Question
{
    public override QuestionType Type => QuestionType.TypeInAnswer;

    public TypeInAnswerQuestion(string text, int index, int timeLimitInSeconds)
        : base(text, index, timeLimitInSeconds)
    { }
}