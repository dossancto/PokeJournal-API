﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokeJournal.Data;

#nullable disable

namespace PokeJournal.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PokeJournal.Models.PokeTeamModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("PokeTeams");
                });

            modelBuilder.Entity("PokeJournal.Models.PokemonListModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CustomName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DefaultName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImgURL")
                        .HasColumnType("longtext");

                    b.Property<Guid>("PokeTeamId")
                        .HasColumnType("char(36)");

                    b.Property<int>("PokemonIndex")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("PokeTeamId");

                    b.ToTable("PokemonLists");
                });

            modelBuilder.Entity("PokeJournal.Models.PokemonListModel", b =>
                {
                    b.HasOne("PokeJournal.Models.PokeTeamModel", "PokeTeam")
                        .WithMany("Pokemons")
                        .HasForeignKey("PokeTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PokeTeam");
                });

            modelBuilder.Entity("PokeJournal.Models.PokeTeamModel", b =>
                {
                    b.Navigation("Pokemons");
                });
#pragma warning restore 612, 618
        }
    }
}
