using Microsoft.EntityFrameworkCore;

namespace Questions.Infrastructure
{
    public class QuestionsContext : DbContext
    {
        public QuestionsContext(DbContextOptions<QuestionsContext> options) : base(options)
        { }
    }
}
