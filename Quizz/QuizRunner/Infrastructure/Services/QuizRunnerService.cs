﻿using AutoMapper;
using Quizz.Common.Models;
using Quizz.QuizRunner.Application.Models;
using Quizz.QuizRunner.Application.ViewModels;
using Quizz.QuizRunner.Infrastructure.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Quizz.QuizRunner.Infrastructure.Services
{
    public class QuizRunnerService : IQuizRunnerService
    {
        // Key: invite code
        // Value: quiz in progress
        private static readonly ConcurrentDictionary<string, Quiz> quizzes = new();
        private readonly IMapper mapper;

        public QuizRunnerService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public bool SubmitAnswer(string inviteCode, Guid questionId, string connectionId, JsonElement rawAnswer)
        {
            if (quizzes.TryGetValue(inviteCode, out Quiz quiz))
            {
                return quiz.SubmitAnswer(questionId, connectionId, rawAnswer);
            }
            throw new QuizRunnerDomainException($"No quiz found with invite code {inviteCode}");
        }

        public void AddParticipant(string inviteCode, string name, string connectionId)
        {
            var participant = new Participant
            {
                Name = name,
                SignalRConnectionId = connectionId,
            };
            if (quizzes.TryGetValue(inviteCode, out Quiz quiz))
            {
                quiz.AddParticipant(participant);
            }
        }

        public Question GetCurrentQuestion(string inviteCode)
        {
            Question question = null;
            if (quizzes.TryGetValue(inviteCode, out Quiz quiz))
            {
                question = quiz.CurrentQuestion;
            }
            return question;
        }

        public string InitQuiz(int quizId, IEnumerable<Question> questions)
        {
            string inviteCode;
            var newQuiz = new Quiz
            {
                Id = quizId,
                Questions = questions,
            };
            do
            {
                inviteCode = GenerateInviteCode(8);
            }
            // Keeps looping until a unique key is generated
            while (!quizzes.TryAdd(inviteCode, newQuiz));
            return inviteCode;
        }

        public void ProgressToNextQuestion(string inviteCode)
        {
            if (quizzes.TryGetValue(inviteCode, out Quiz quiz))
            {
                quiz.ProgressToNextQuestion();
            }
        }

        public bool QuizExists(string inviteCode)
        {
            return quizzes.ContainsKey(inviteCode);
        }

        private string GenerateInviteCode(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                // RandomNumberGenerator generates cryptographically strong random values
                var ch = chars[RandomNumberGenerator.GetInt32(chars.Length)];
                sb.Append(ch);
            }
            return sb.ToString();
        }

        public QuizResultsViewModel GetQuizResults(string inviteCode)
        {
            if (quizzes.TryGetValue(inviteCode, out Quiz quiz))
            {
                var results = quiz.GetResults();
                return mapper.Map<QuizResultsViewModel>(results);
            }
            throw new QuizRunnerDomainException($"No quiz found with invite code {inviteCode}");
        }

        public void EndQuiz(string inviteCode)
        {
            quizzes.TryRemove(inviteCode, out Quiz quiz);
        }
    }
}