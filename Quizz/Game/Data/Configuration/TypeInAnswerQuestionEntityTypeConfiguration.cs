using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizz.Common.Models;
using Quizz.GameService.Application.Models;

namespace Quizz.GameService.Data.Configuration
{
    public class TypeInAnswerQuestionEntityTypeConfiguration
        : IEntityTypeConfiguration<TypeInAnswerQuestion>
    {
        public void Configure(EntityTypeBuilder<TypeInAnswerQuestion> builder)
        {
            builder.HasMany(question => question.AcceptedAnswers)
                .WithOne()
                .HasForeignKey(answer => answer.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}