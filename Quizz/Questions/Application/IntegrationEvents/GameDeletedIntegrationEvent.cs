using Quizz.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quizz.Questions.Application.IntegrationEvents
{
    public record GameDeletedIntegrationEvent : IntegrationEvent
    {
        public IEnumerable<Guid> QuestionIds { get; private set; } = Enumerable.Empty<Guid>();

        public GameDeletedIntegrationEvent(IEnumerable<Guid> questionIds)
        {
            QuestionIds = questionIds;
        }
    }
}