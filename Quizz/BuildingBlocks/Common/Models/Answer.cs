using Quizz.Common.DataAccess;
using System;

namespace Quizz.Common.Models;

public class Answer : Entity<int>
{
    public string Text { get; set; }

    public bool IsCorrect { get; set; }

    public int Index { get; set; }

    public Guid QuestionId { get; set; }
}