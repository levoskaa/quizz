using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizz.GameService.Application.Models;

namespace Quizz.GameService.Data.Configuration
{
    public class GameEntityTypeConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Game");

            builder.HasKey(game => game.Id);

            builder.Property(game => game.Id)
                .UseHiLo("gameseq");

            builder.HasMany(game => game.GameQuestions)
                .WithOne()
                .HasForeignKey(gameQuestion => gameQuestion.GameId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}