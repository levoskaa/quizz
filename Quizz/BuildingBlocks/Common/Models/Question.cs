using Quizz.Common.DataAccess;
using System;
using System.Collections.Generic;

namespace Quizz.Common.Models;

public abstract class Question : Entity<Guid>, IAggregateRoot
{
    public string Text { get; set; }

    public virtual QuestionType Type { get; }

    public int Index { get; set; }

    public int TimeLimitInSeconds { get; set; }

    protected readonly List<Answer> answerPossibilities;
    public IReadOnlyCollection<Answer> AnswerPossibilities => answerPossibilities.AsReadOnly();

    public Question()
    {
        answerPossibilities = new List<Answer>();
    }

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
}