using System.Linq;
using System.Text.Json;

namespace Quizz.Common.Models;

public class FindOrderQuestion : Question
{
    public override QuestionType Type => QuestionType.FindOrder;

    public FindOrderQuestion(string text, int index, int timeLimitInSeconds)
        : base(text, index, timeLimitInSeconds)
    { }

    // Parameter answer is an array of Answer ids
    public override bool CheckAnswer(JsonElement rawAnswer)
    {
        var answer = rawAnswer.Deserialize<int[]>();
        var acceptedSolution = AnswerPossibilities
            .Cast<FindOrderAnswer>()
            .OrderBy(answer => answer.CorrectIndex)
            .ToList();
        for (int i = 0; i < acceptedSolution.Count; i++)
        {
            if (answer[i] != acceptedSolution[i].Id)
            {
                return false;
            }
        }
        return true;
    }
}