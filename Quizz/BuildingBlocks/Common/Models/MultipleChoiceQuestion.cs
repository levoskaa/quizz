using System.Collections.Generic;

namespace Quizz.Common.Models;

public class MultipleChoiceQuestion : Question
{
    public override QuestionType Type => QuestionType.MultipleChoice;

    public override IReadOnlyCollection<MultipleChoiceAnswer> AnswerPossibilities
    => (IReadOnlyCollection<MultipleChoiceAnswer>)answerPossibilites.AsReadOnly();

    public MultipleChoiceQuestion(string text, int index, int timeLimitInSeconds)
        : base(text, index, timeLimitInSeconds)
    { }
}