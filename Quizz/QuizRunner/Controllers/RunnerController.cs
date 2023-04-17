using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizz.Common.Services;
using Quizz.Common.ViewModels;
using Quizz.QuizRunner.Application.Commands;
using Quizz.QuizRunner.Application.Dtos;
using Quizz.QuizRunner.Application.ViewModels;
using System.Net;
using System.Threading.Tasks;

namespace Quizz.QuizRunner.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RunnerController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IIdentityService identityService;

        public RunnerController(IMapper mapper, IMediator mediator, IIdentityService identityService)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.identityService = identityService;
        }

        [HttpPost("init")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorViewModel), (int)HttpStatusCode.BadRequest)]
        public Task<GameInitializedViewModel> InitGame([FromBody] InitGameDto initGameDto)
        {
            var initGameCommand = mapper.Map<InitGameCommand>(initGameDto);
            initGameCommand.UserId = identityService.GetUserIdentity();
            return mediator.Send(initGameCommand);
        }
    }
}