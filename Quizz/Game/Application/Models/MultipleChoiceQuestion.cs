using System.Collections.Generic;

namespace Quizz.GameService.Application.Models;

public class MultipleChoiceQuestion : Question
{
    public override QuestionType Type => QuestionType.MultipleChoice;

    private readonly List<Answer> possibleAnswers;
    public IReadOnlyCollection<Answer> PossibleAnswers => possibleAnswers.AsReadOnly();

    private readonly List<int> correctAnswerIds;
    public IReadOnlyCollection<int> CorrectAnswerIds => correctAnswerIds.AsReadOnly();

    public MultipleChoiceQuestion()
    {
        possibleAnswers = new List<Answer>();
        correctAnswerIds = new List<int>();
    }
}
