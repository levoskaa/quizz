﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quizz.GameService.Data;

#nullable disable

namespace GameService.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20220305164741_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.HasSequence("answerseq")
                .IncrementsBy(10);

            modelBuilder.HasSequence("gameseq")
                .IncrementsBy(10);

            modelBuilder.Entity("Quizz.GameService.Application.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "answerseq");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answer", (string)null);
                });

            modelBuilder.Entity("Quizz.GameService.Application.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "gameseq");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Game", (string)null);
                });

            modelBuilder.Entity("Quizz.GameService.Application.Models.GameQuestion", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("GameId1")
                        .HasColumnType("int");

                    b.HasKey("GameId", "QuestionId");

                    b.HasIndex("GameId1");

                    b.HasIndex("QuestionId")
                        .IsUnique();

                    b.ToTable("GameQuestion", (string)null);
                });

            modelBuilder.Entity("Quizz.GameService.Application.Models.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TimeLimitInSeconds")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Question", (string)null);

                    b.HasDiscriminator<int>("Type");
                });

            modelBuilder.Entity("Quizz.GameService.Application.Models.FindCorrectOrderQuestion", b =>
                {
                    b.HasBaseType("Quizz.GameService.Application.Models.Question");

                    b.Property<string>("CorrectIdOrder")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Quizz.GameService.Application.Models.MultipleChoiceQuestion", b =>
                {
                    b.HasBaseType("Quizz.GameService.Application.Models.Question");

                    b.Property<string>("CorrectAnswerIds")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Quizz.GameService.Application.Models.TrueOrFalseQuestion", b =>
                {
                    b.HasBaseType("Quizz.GameService.Application.Models.Question");

                    b.Property<bool>("CorrectAnswer")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Quizz.GameService.Application.Models.TypeInAnswerQuestion", b =>
                {
                    b.HasBaseType("Quizz.GameService.Application.Models.Question");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("Quizz.GameService.Application.Models.Answer", b =>
                {
                    b.HasOne("Quizz.GameService.Application.Models.FindCorrectOrderQuestion", null)
                        .WithMany("AnswerPossibilities")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Quizz.GameService.Application.Models.MultipleChoiceQuestion", null)
                        .WithMany("PossibleAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Quizz.GameService.Application.Models.TypeInAnswerQuestion", null)
                        .WithMany("AcceptedAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Quizz.GameService.Application.Models.GameQuestion", b =>
                {
                    b.HasOne("Quizz.GameService.Application.Models.Game", null)
                        .WithMany("GameQuestions")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Quizz.GameService.Application.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId1");

                    b.HasOne("Quizz.GameService.Application.Models.Question", "Question")
                        .WithOne()
                        .HasForeignKey("Quizz.GameService.Application.Models.GameQuestion", "QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Quizz.GameService.Application.Models.Question", b =>
                {
                    b.HasOne("Quizz.GameService.Application.Models.Game", null)
                        .WithMany("Questions")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("Quizz.GameService.Application.Models.Game", b =>
                {
                    b.Navigation("GameQuestions");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Quizz.GameService.Application.Models.FindCorrectOrderQuestion", b =>
                {
                    b.Navigation("AnswerPossibilities");
                });

            modelBuilder.Entity("Quizz.GameService.Application.Models.MultipleChoiceQuestion", b =>
                {
                    b.Navigation("PossibleAnswers");
                });

            modelBuilder.Entity("Quizz.GameService.Application.Models.TypeInAnswerQuestion", b =>
                {
                    b.Navigation("AcceptedAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}