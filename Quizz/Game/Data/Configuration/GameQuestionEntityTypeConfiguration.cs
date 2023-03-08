using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizz.GameService.Application.Models;

namespace Quizz.GameService.Data.Configuration;

public class GameQuestionEntityTypeConfiguration : IEntityTypeConfiguration<GameQuestion>
{
    public void Configure(EntityTypeBuilder<GameQuestion> builder)
    {
        builder.ToTable("GameQuestion");

        // Create composite key
        builder.HasKey(gq => new { gq.GameId, gq.QuestionId });

        builder.HasOne(gq => gq.Game)
            .WithMany(nameof(Game.GameQuestions))
            .HasForeignKey(nameof(GameQuestion.GameId))
            .OnDelete(DeleteBehavior.Cascade);
    }
}