using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quizz.GameService.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quizz.GameService.Data.Configuration;

public class MultipleChoiceQuestionEntityTypeConfiguration
    : IEntityTypeConfiguration<MultipleChoiceQuestion>
{
    public void Configure(EntityTypeBuilder<MultipleChoiceQuestion> builder)
    {
        builder.HasMany(question => question.PossibleAnswers)
            .WithOne()
            .HasForeignKey(answer => answer.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Map list to string
        var converter = new ValueConverter<IReadOnlyCollection<int>, string>(
            v => string.Join(',', v),
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                  .Select(x => int.Parse(x))
                  .ToList()
                  .AsReadOnly()
        );
        builder.Property(question => question.CorrectAnswerIds)
            .HasConversion(converter);
    }
}
