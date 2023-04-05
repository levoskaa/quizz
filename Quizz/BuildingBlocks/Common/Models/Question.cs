using Quizz.Common.DataAccess;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Quizz.Common.Models;

public abstract class Question : Entity<Guid>, IAggregateRoot
{
    public string Text { get; set; }

    public virtual QuestionType Type { get; }

    public int Index { get; set; }

    public int TimeLimitInSeconds { get; set; }

    protected readonly List<Answer> answerPossibilities = new();
    public IReadOnlyCollection<Answer> AnswerPossibilities => answerPossibilities.AsReadOnly();

    public Question()
    { }

    public Question(string text, int index, int timeLimitInSeconds)
        : this()
    {
        Text = text;
        Index = index;
        TimeLimitInSeconds = timeLimitInSeconds;
    }

    // TODO: check after refactoring is done
    public void ReplaceAnswerPossibilities(IEnumerable<Answer> answers)
    {
        answerPossibilities.Clear();
        answerPossibilities.AddRange(answers);
    }

    public abstract bool CheckAnswer(JsonElement rawAnswer);
}