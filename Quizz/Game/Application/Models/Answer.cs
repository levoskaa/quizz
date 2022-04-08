using Quizz.Common.DataAccess;
using System;

namespace Quizz.GameService.Application.Models;

public class Answer : Entity<int>
{
    public string Text { get; set; }

    public Guid QuestionId { get; set; }
}
