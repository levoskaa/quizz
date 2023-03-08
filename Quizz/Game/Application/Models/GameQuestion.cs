﻿using System;

namespace Quizz.GameService.Application.Models;

public class GameQuestion
{
    public int GameId { get; set; }
    public Game Game { get; set; }

    public Guid QuestionId { get; set; }
}