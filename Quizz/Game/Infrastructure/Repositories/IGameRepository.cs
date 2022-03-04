using Quizz.Common.DataAccess;
using Quizz.GameService.Application.Models;

namespace Quizz.GameService.Infrastructure.Repositories
{
    public interface IGameRepository : IRepository<Game>
    {
        public int AddGame(Game game);
    }
}