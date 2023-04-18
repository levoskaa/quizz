using Quizz.BuildingBlocks.EventBus.Abstractions;
using Quizz.Common.Models;
using Quizz.Questions.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizz.Questions.Application.IntegrationEvents
{
    public class GameDeletedIntegrationEventHandler : IIntegrationEventHandler<GameDeletedIntegrationEvent>
    {
        private readonly IQuestionRepository questionRepository;

        public GameDeletedIntegrationEventHandler(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public async Task Handle(GameDeletedIntegrationEvent @event)
        {
            var questions = await GetQuestions(@event.QuestionIds.ToList());
            foreach (var question in questions)
            {
                questionRepository.Remove(question);
            }
            await questionRepository.UnitOfWork.SaveChangesAsync();
        }

        private async Task<IEnumerable<Question>> GetQuestions(List<Guid> questionIds)
        {
            var questions = await questionRepository.FilterAsync(question => questionIds.Contains(question.Id));
            return questions;
        }
    }
}