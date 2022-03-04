using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizz.Common.ViewModels;
using Quizz.GameService.Application.Commands;
using System.Threading.Tasks;

namespace Quizz.GameService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GamesController : ControllerBase
    {
        private readonly IMediator mediator;

        public GamesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<EntityCreatedViewModel<int>> CreateGame([FromBody] CreateGameCommand createGameCommand)
        {
            var createdId = await mediator.Send(createGameCommand);
            return new EntityCreatedViewModel<int>
            {
                Id = createdId
            };
        }
    }
}