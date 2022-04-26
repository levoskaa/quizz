﻿using Quizz.Common.DataAccess;
using System;
using System.Collections.Generic;

namespace Quizz.Common.Models;

public abstract class Question : Entity<Guid>, IAggregateRoot
{
    public string Text { get; set; }

    public virtual QuestionType Type { get; private set; }

    public int Index { get; set; }

    public int TimeLimitInSeconds { get; set; }

    private readonly List<Answer> answerPossibilites;
    public IReadOnlyCollection<Answer> AnswerPossibilities => answerPossibilites.AsReadOnly();

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

    public void ReplaceAnswerPossibilities(IEnumerable<Answer> answers)
    {
        answerPossibilites.Clear();
        answerPossibilites.AddRange(answers);
    }
}