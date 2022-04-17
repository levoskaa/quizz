namespace Quizz.Common.Models;

public class TypeInAnswerQuestion : Question
{
    public override QuestionType Type => QuestionType.TypeInAnswer;
}