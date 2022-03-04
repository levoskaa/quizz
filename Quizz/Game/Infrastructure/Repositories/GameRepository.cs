using Quizz.Common.DataAccess;
using Quizz.GameService.Application.Models;

namespace Quizz.GameService.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GameContext context;

        public IUnitOfWork UnitOfWork => context;

        public GameRepository(GameContext context)
        {
            this.context = context;
        }

        public int AddGame(Game game)
        {
            var entry = context.Games.Add(game);
            return entry.Entity.Id;
        }
    }
}