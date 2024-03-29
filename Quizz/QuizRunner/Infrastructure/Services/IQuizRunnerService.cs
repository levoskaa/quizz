﻿using Quizz.Common.Models;
using Quizz.QuizRunner.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Quizz.QuizRunner.Infrastructure.Services
{
    public interface IQuizRunnerService
    {
        string InitQuiz(int quizId, IEnumerable<Question> questions);

        bool QuizExists(string inviteCode);

        void AddParticipant(string inviteCode, string name, string connectionId);

        Question GetCurrentQuestion(string inviteCode);

        void ProgressToNextQuestion(string inviteCode);

        bool SubmitAnswer(string inviteCode, Guid questionId, string connectionId, JsonElement rawAnswer);

        QuizResultsViewModel GetQuizResults(string inviteCode);

        void EndQuiz(string inviteCode);
    }
}