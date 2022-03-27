using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizz.Common.Models;
using Quizz.GameService.Application.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizz.GameService.Data.Configuration
{
    public class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Question")
                .HasDiscriminator(question => question.Type)
                .HasValue<FindCorrectOrderQuestion>(QuestionType.FindCorrectOrder)
                .HasValue<MultipleChoiceQuestion>(QuestionType.MultipleChoice)
                .HasValue<TrueOrFalseQuestion>(QuestionType.TrueOrFalse)
                .HasValue<TypeInAnswerQuestion>(QuestionType.TypeInAnswer);

            builder.HasKey(question => question.Id);

            builder.Property(question => question.Id)
                .ValueGeneratedOnAdd();
        }
    }
}