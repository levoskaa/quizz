using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizz.Common.Models;

namespace Quizz.GameService.Data.Configuration
{
    public class AnswerEntityTypeConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable("Answer")
                .HasDiscriminator<string>("AnswerType")
                .HasValue<Answer>("base")
                .HasValue<FindOrderAnswer>("find_order")
                .HasValue<MultipleChoiceAnswer>("multiple_choice");

            builder.HasKey(answer => answer.Id);

            builder.Property(c => c.Id)
               .UseHiLo("answerseq");
        }
    }
}