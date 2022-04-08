using Microsoft.EntityFrameworkCore;

namespace Quizz.Questions.Infrastructure;

public class QuestionsContext : DbContext
{
    public QuestionsContext(DbContextOptions<QuestionsContext> options) : base(options)
    { }
}
