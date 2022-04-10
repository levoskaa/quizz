using Quizz.Common.DataAccess;
using Quizz.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quizz.GameService.Application.Models;

public class Game : Entity<int>, IAggregateRoot
{
    public string Name { get; set; }

    public string OwnerId { get; set; }

    public DateTime UpdatedAt { get; set; }

    private readonly List<GameQuestion> gameQuestions;
    public IReadOnlyCollection<GameQuestion> GameQuestions => gameQuestions.AsReadOnly();

    public IReadOnlyCollection<Question> Questions
    {
        get
        {
            return gameQuestions.Select(x => x.Question)
                .ToList()
                .AsReadOnly();
        }
    }

    public IReadOnlyCollection<Guid> QuestionIds
    {
        get
        {
            return gameQuestions.Select(x => x.QuestionId)
                .ToList()
                .AsReadOnly();
        }
    }

    public Game()
    {
        gameQuestions = new List<GameQuestion>();
    }
}
