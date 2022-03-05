using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quizz.Common.ViewModels;
using Quizz.GameService.Application.Commands;
using System.Net;
using System.Threading.Tasks;

namespace Quizz.GameService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    public class GamesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor contextAccessor;

        public GamesController(IMediator mediator, IHttpContextAccessor contextAccessor)
        {
            this.mediator = mediator;
            this.contextAccessor = contextAccessor;
        }

        [HttpPost]
        [ProducesResponseType(typeof(EntityCreatedViewModel<int>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorViewModel), (int)HttpStatusCode.BadRequest)]
        public async Task<EntityCreatedViewModel<int>> CreateGame([FromBody] CreateGameCommand createGameCommand)
        {
            var createdId = await mediator.Send(createGameCommand);
            contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
            return new EntityCreatedViewModel<int>
            {
                Id = createdId
            };
        }
    }
}