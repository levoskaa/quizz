using Microsoft.EntityFrameworkCore;

namespace Quizz.Game.Infrastructure
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        { }
    }
}
