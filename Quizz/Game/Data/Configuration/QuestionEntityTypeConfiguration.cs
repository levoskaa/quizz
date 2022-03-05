using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizz.GameService.Application.Models;

namespace Quizz.GameService.Data.Configuration
{
    public class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Question")
                .HasDiscriminator(question => question.Type);

            builder.HasKey(question => question.Id);

            builder.Property(question => question.Id)
                .HasDefaultValue("newsequentialid()");
        }
    }
}