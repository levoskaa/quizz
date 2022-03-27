using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quizz.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quizz.GameService.Data.Configuration
{
    public class FindCorrectOrderQuestionEntityTypeConfiguration
        : IEntityTypeConfiguration<FindCorrectOrderQuestion>
    {
        public void Configure(EntityTypeBuilder<FindCorrectOrderQuestion> builder)
        {
            builder.HasMany(question => question.AnswerPossibilities)
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
            builder.Property(question => question.CorrectIdOrder)
                .HasConversion(converter);
        }
    }
}