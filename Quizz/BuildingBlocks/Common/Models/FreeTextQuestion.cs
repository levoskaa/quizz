using System.Linq;
using System.Text.Json;

namespace Quizz.Common.Models;

public class FreeTextQuestion : Question
{
    public override QuestionType Type => QuestionType.FreeText;

    public FreeTextQuestion(string text, int index, int timeLimitInSeconds)
        : base(text, index, timeLimitInSeconds)
    { }

    // Parameter answer is a string
    public override bool CheckAnswer(JsonElement rawAnswer)
    {
        var answer = rawAnswer.GetString();
        return AnswerPossibilities.Any(acceptedAnswer => answer == acceptedAnswer.Text);
    }
}