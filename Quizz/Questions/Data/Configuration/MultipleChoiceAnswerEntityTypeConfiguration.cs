using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizz.Common.Models;

namespace Quizz.Questions.Data.Configuration
{
    public class MultipleChoiceAnswerEntityTypeConfiguration : IEntityTypeConfiguration<MultipleChoiceAnswer>
    {
        public void Configure(EntityTypeBuilder<MultipleChoiceAnswer> builder)
        {
            builder.Property(answer => answer.DisplayIndex)
                .HasColumnName("DisplayIndex");
        }
    }
}