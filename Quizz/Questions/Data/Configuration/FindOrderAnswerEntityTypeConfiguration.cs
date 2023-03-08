using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quizz.Common.Models;

namespace Quizz.Questions.Data.Configuration
{
    public class FindOrderAnswerEntityTypeConfiguration : IEntityTypeConfiguration<FindOrderAnswer>
    {
        public void Configure(EntityTypeBuilder<FindOrderAnswer> builder)
        {
            builder.Property(answer => answer.DisplayIndex)
                .HasColumnName("DisplayIndex");
        }
    }
}
