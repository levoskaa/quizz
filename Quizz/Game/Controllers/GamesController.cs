using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quizz.Common.ErroHandling;
using Quizz.Common.ErrorHandling;
using Quizz.Common.Services;
using Quizz.Common.ViewModels;
using Quizz.GameService.Application.Commands;
using Quizz.GameService.Application.Dtos;
using Quizz.GameService.Application.Models;
using Quizz.GameService.Application.ViewModels;
using Quizz.GameService.Data;
using System.Collections.Generic;
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
        private readonly DapperContext dapper;

        public GamesController(
            IMediator mediator,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            IIdentityService identityService,
            DapperContext dapper)
        {
            this.mediator = mediator;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
            this.identityService = identityService;
            this.dapper = dapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(EntityCreatedViewModel<int>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ErrorViewModel), (int)HttpStatusCode.BadRequest)]
        public async Task<EntityCreatedViewModel<int>> CreateGame([FromBody] CreateGameDto createGameDto)
        {
            contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
            var createGameCommand = mapper.Map<CreateGameCommand>(createGameDto);
            createGameCommand.UserId = identityService.GetUserIdentity();
            var createdId = await mediator.Send(createGameCommand);
            return new EntityCreatedViewModel<int>
            {
                Id = createdId
            };
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<GameViewModel>), (int)HttpStatusCode.OK)]
        public async Task<PaginatedItemsViewModel<GameViewModel>> GetGames([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var userId = identityService.GetUserIdentity();
            var offset = pageSize * pageIndex;
            var gamesQuery = @"SELECT * FROM Game
                             WHERE OwnerId=@userId
                             ORDER BY UpdatedAt DESC
                             OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY;";
            var gameCountQuery = @"SELECT COUNT(*) FROM Game
                                 WHERE OwnerId=@userId;";
            using (var connection = dapper.CreateConnection())
            {
                var games = await connection.QueryAsync<Game>(gamesQuery, new { userId, offset, pageSize });
                var totalCount = await connection.QuerySingleAsync<long>(gameCountQuery, new { userId });
                return new PaginatedItemsViewModel<GameViewModel>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    mapper.Map<IEnumerable<GameViewModel>>(games));
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<GameViewModel> GetGame([FromRoute] int id)
        {
            var userId = identityService.GetUserIdentity();
            var gameQuery = @"SELECT * FROM Game
                              WHERE OwnerId=@userId
                              AND Id=@gameId;";
            using (var connection = dapper.CreateConnection())
            {
                var game = await connection.QuerySingleOrDefaultAsync<Game>(gameQuery, new { userId, gameId = id });
                if (game == null)
                {
                    throw new EntityNotFoundException($"Game with id {id} not found", ValidationError.GameNotFound);
                }
                return mapper.Map<GameViewModel>(game);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorViewModel), (int)HttpStatusCode.BadRequest)]
        public Task UpdateGame([FromBody] UpdateGameDto updateGameDto, [FromRoute(Name = "id")] int gameId)
        {
            var updateGameCommand = mapper.Map<UpdateGameCommand>(updateGameDto);
            updateGameCommand.GameId = gameId;
            updateGameCommand.UserId = identityService.GetUserIdentity();
            return mediator.Send(updateGameCommand);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorViewModel), (int)HttpStatusCode.BadRequest)]
        public Task DeleteGame([FromRoute(Name = "id")] int gameId)
        {
            contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            var userId = identityService.GetUserIdentity();
            var deleteGameCommand = new DeleteGameCommand(gameId, userId);
            return mediator.Send(deleteGameCommand);
        }
    }
}