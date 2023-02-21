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

    protected readonly List<Answer> answerPossibilites;
    public virtual IReadOnlyCollection<Answer> AnswerPossibilities => answerPossibilites.AsReadOnly();

    public Question()
    {
        answerPossibilites = new List<Answer>();
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
        answerPossibilites.Clear();
        answerPossibilites.AddRange(answers);
    }
}