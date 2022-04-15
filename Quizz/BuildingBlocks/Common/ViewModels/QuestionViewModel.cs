using Quizz.Common.Models;

namespace Quizz.Common.ViewModels;

public class QuestionViewModel
{
    public string Id { get; init; }
    public string Text { get; init; }
    public QuestionType Type { get; init; }
    public int Index { get; init; }
    public int TimeLimitInSeconds { get; init; }
    public Answer AnswerPossibilites { get; init; }
    public int CorrectIdOrder { get; init; }
    public int CorrectAnswerIds { get; init; }
    public bool CorrectAnswer { get; init; }
    public Answer AcceptedAnswers { get; init; }
}