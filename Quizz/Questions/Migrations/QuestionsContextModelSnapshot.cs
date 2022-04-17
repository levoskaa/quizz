﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quizz.Questions.Data;

#nullable disable

namespace Questions.Migrations
{
    [DbContext(typeof(QuestionsContext))]
    partial class QuestionsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.HasSequence("answerseq")
                .IncrementsBy(10);

            modelBuilder.Entity("Quizz.Common.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "answerseq");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answer", (string)null);
                });

            modelBuilder.Entity("Quizz.Common.Models.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TimeLimitInSeconds")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Question", (string)null);

                    b.HasDiscriminator<int>("Type");
                });

            modelBuilder.Entity("Quizz.Common.Models.FindCorrectOrderQuestion", b =>
                {
                    b.HasBaseType("Quizz.Common.Models.Question");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Quizz.Common.Models.MultipleChoiceQuestion", b =>
                {
                    b.HasBaseType("Quizz.Common.Models.Question");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Quizz.Common.Models.TrueOrFalseQuestion", b =>
                {
                    b.HasBaseType("Quizz.Common.Models.Question");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Quizz.Common.Models.TypeInAnswerQuestion", b =>
                {
                    b.HasBaseType("Quizz.Common.Models.Question");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("Quizz.Common.Models.Answer", b =>
                {
                    b.HasOne("Quizz.Common.Models.Question", null)
                        .WithMany("AnswerPossibilities")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Quizz.Common.Models.Question", b =>
                {
                    b.Navigation("AnswerPossibilities");
                });
#pragma warning restore 612, 618
        }
    }
}
