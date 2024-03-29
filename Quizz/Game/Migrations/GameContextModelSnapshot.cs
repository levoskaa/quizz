﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quizz.GameService.Data;

#nullable disable

namespace GameService.Migrations
{
    [DbContext(typeof(GameContext))]
    partial class GameContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.HasSequence("gameseq")
                .IncrementsBy(10);

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

                    b.HasKey("GameId", "QuestionId");

                    b.ToTable("GameQuestion", (string)null);
                });

            modelBuilder.Entity("Quizz.GameService.Application.Models.GameQuestion", b =>
                {
                    b.HasOne("Quizz.GameService.Application.Models.Game", "Game")
                        .WithMany("GameQuestions")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Quizz.GameService.Application.Models.Game", b =>
                {
                    b.Navigation("GameQuestions");
                });
#pragma warning restore 612, 618
        }
    }
}
