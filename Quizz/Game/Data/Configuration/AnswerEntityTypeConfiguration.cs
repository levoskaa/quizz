using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizz.GameService.Application.Models;

namespace Quizz.GameService.Data.Configuration;

public class AnswerEntityTypeConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.ToTable("Answer");

        builder.HasKey(answer => answer.Id);

        builder.Property(c => c.Id)
           .UseHiLo("answerseq");
    }
}
