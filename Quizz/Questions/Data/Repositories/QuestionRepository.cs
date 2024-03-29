﻿using Microsoft.EntityFrameworkCore;
using Quizz.Common.DataAccess;
using Quizz.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Quizz.Questions.Data.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly QuestionsContext context;

    public IUnitOfWork UnitOfWork => context;

    public QuestionRepository(QuestionsContext context)
    {
        this.context = context;
    }

    public Guid Add(Question question)
    {
        var entry = context.Questions.Add(question);
        return entry.Entity.Id;
    }

    public async Task<IEnumerable<Question>> FilterAsync(Expression<Func<Question, bool>> filter)
    {
        var questions = await context.Questions
            .Where(filter)
            .Include(question => question.AnswerPossibilities)
            .ToListAsync();
        return questions;
    }

    public void Remove(Question question)
    {
        context.Questions.Remove(question);
    }
}