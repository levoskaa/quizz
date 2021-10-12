using Microsoft.EntityFrameworkCore;

namespace Game.Infrastructure
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        { }
    }
}
