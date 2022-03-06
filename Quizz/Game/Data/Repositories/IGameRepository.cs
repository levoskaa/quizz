using Quizz.Common.DataAccess;
using Quizz.GameService.Application.Models;
using System.Threading.Tasks;

namespace Quizz.GameService.Data.Repositories
{
    public interface IGameRepository : IRepository<Game>
    {
        int Add(Game game);

        Task<Game> GetAsync(int id);

        void Update(Game game);

        void Remove(Game game);
    }
}