using Microsoft.EntityFrameworkCore;
using Quizz.Common.DataAccess;
using Quizz.Common.ErroHandling;
using Quizz.Common.ErrorHandling;
using Quizz.GameService.Application.Models;
using System.Threading.Tasks;

namespace Quizz.GameService.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GameContext context;

        public IUnitOfWork UnitOfWork => context;

        public GameRepository(GameContext context)
        {
            this.context = context;
        }

        public int Add(Game game)
        {
            var entry = context.Games.Add(game);
            return entry.Entity.Id;
        }

        public async Task<Game> GetAsync(int id)
        {
            var game = await context.Games
                .AsNoTracking()
                .SingleOrDefaultAsync(game => game.Id == id);
            if (game == null)
            {
                throw new EntityNotFoundException(
                    $"Game with id {id} not found",
                    ValidationError.GameNotFound);
            }
            return game;
        }

        public void Update(Game game)
        {
            context.Games.Update(game);
        }

        public void Remove(Game game)
        {
            context.Games.Remove(game);
        }
    }
}