﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Puissance4.DataAccess;

#nullable disable

namespace Puissance4.DataAccess.Migrations
{
    [DbContext(typeof(Puissance4DbContext))]
    partial class Puissance4DbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("Puissance4.DataAccess.Entities.EFCell", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Column")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GridId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Row")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TokenId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GridId");

                    b.HasIndex("TokenId");

                    b.ToTable("Cells");
                });

            modelBuilder.Entity("Puissance4.DataAccess.Entities.EFGame", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CurrentTurnId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GuestId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HostId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("WinnerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CurrentTurnId");

                    b.HasIndex("GuestId");

                    b.HasIndex("HostId");

                    b.HasIndex("WinnerId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Puissance4.DataAccess.Entities.EFGrid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Columns")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rows")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Grids");
                });

            modelBuilder.Entity("Puissance4.DataAccess.Entities.EFPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Puissance4.DataAccess.Entities.EFToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Puissance4.DataAccess.Entities.EFCell", b =>
                {
                    b.HasOne("Puissance4.DataAccess.Entities.EFGrid", "Grid")
                        .WithMany("Cells")
                        .HasForeignKey("GridId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Puissance4.DataAccess.Entities.EFToken", "Token")
                        .WithMany()
                        .HasForeignKey("TokenId");

                    b.Navigation("Grid");

                    b.Navigation("Token");
                });

            modelBuilder.Entity("Puissance4.DataAccess.Entities.EFGame", b =>
                {
                    b.HasOne("Puissance4.DataAccess.Entities.EFPlayer", "CurrentTurn")
                        .WithMany()
                        .HasForeignKey("CurrentTurnId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Puissance4.DataAccess.Entities.EFPlayer", "Guest")
                        .WithMany("GamesAsGuest")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Puissance4.DataAccess.Entities.EFPlayer", "Host")
                        .WithMany("GamesAsHost")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Puissance4.DataAccess.Entities.EFGrid", "Grid")
                        .WithOne()
                        .HasForeignKey("Puissance4.DataAccess.Entities.EFGame", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Puissance4.DataAccess.Entities.EFPlayer", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CurrentTurn");

                    b.Navigation("Grid");

                    b.Navigation("Guest");

                    b.Navigation("Host");

                    b.Navigation("Winner");
                });

            modelBuilder.Entity("Puissance4.DataAccess.Entities.EFGrid", b =>
                {
                    b.Navigation("Cells");
                });

            modelBuilder.Entity("Puissance4.DataAccess.Entities.EFPlayer", b =>
                {
                    b.Navigation("GamesAsGuest");

                    b.Navigation("GamesAsHost");
                });
#pragma warning restore 612, 618
        }
    }
}
