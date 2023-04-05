using System.Linq;
using System.Text.Json;

namespace Quizz.Common.Models;

public class MultipleChoiceQuestion : Question
{
    public override QuestionType Type => QuestionType.MultipleChoice;

    public MultipleChoiceQuestion(string text, int index, int timeLimitInSeconds)
        : base(text, index, timeLimitInSeconds)
    { }

    // Parameter answer is an array of Answer ids
    public override bool CheckAnswer(JsonElement rawAnswer)
    {
        var answer = rawAnswer.Deserialize<int[]>();
        var answerPossibilities = AnswerPossibilities.Cast<MultipleChoiceAnswer>().ToList();
        var allCorrectAnswersSelected = answerPossibilities
            .Where(answer => answer.IsCorrect)
            .All(correctAnswer => answer.Contains(correctAnswer.Id));
        var noIncorrectAnswersSelected = answerPossibilities
            .Where(answer => !answer.IsCorrect)
            .All(incorrectAnswer => !answer.Contains(incorrectAnswer.Id));
        return allCorrectAnswersSelected && noIncorrectAnswersSelected;
    }
}