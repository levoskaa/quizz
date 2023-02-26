namespace Quizz.Common.Models;

public class MultipleChoiceQuestion : Question
{
    public override QuestionType Type => QuestionType.MultipleChoice;

    public MultipleChoiceQuestion(string text, int index, int timeLimitInSeconds)
        : base(text, index, timeLimitInSeconds)
    { }
}