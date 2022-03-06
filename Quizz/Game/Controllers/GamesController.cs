using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quizz.Common.Services;
using Quizz.Common.ViewModels;
using Quizz.GameService.Application.Commands;
using Quizz.GameService.Application.Dtos;
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
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;

        public GamesController(
            IMediator mediator,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            IIdentityService identityService)
        {
            this.mediator = mediator;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
            this.identityService = identityService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(EntityCreatedViewModel<int>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorViewModel), (int)HttpStatusCode.BadRequest)]
        public async Task<EntityCreatedViewModel<int>> CreateGame([FromBody] CreateGameDto createGameDto)
        {
            var createGameCommand = mapper.Map<CreateGameCommand>(createGameDto);
            createGameCommand.UserId = identityService.GetUserIdentity();
            var createdId = await mediator.Send(createGameCommand);
            contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
            return new EntityCreatedViewModel<int>
            {
                Id = createdId
            };
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorViewModel), (int)HttpStatusCode.BadRequest)]
        public Task UpdateGame([FromBody] UpdateGameDto updateGameDto, [FromRoute(Name = "id")] int gameId)
        {
            var updateGameCommand = mapper.Map<UpdateGameCommand>(updateGameDto);
            updateGameCommand.GameId = gameId;
            updateGameCommand.UserId = identityService.GetUserIdentity();
            return mediator.Send(updateGameCommand);
        }
    }
}