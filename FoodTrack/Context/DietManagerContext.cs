﻿using System;
using FoodTrack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FoodTrack.Context
{
    public partial class DietManagerContext : DbContext
    {
        public DietManagerContext()
        {
        }

        public DietManagerContext(DbContextOptions<DietManagerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FoodCategory> FoodCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersDatum> UsersData { get; set; }
        public virtual DbSet<UsersParam> UsersParams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-A8E6OVC;Database=DietManager;Trusted_Connection=True;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<FoodCategory>(entity =>
            {
                entity.HasIndex(e => e.CategoryName, "UQ__FoodCate__8517B2E006DE4F2E")
                    .IsUnique();

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CaloriesGram)
                    .HasColumnType("decimal(7, 2)")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.CarbohydratesGram)
                    .HasColumnType("decimal(7, 2)")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.FatsGram)
                    .HasColumnType("decimal(7, 2)")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.FoodCategory).HasMaxLength(50);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ProteinsGram)
                    .HasColumnType("decimal(7, 2)")
                    .HasDefaultValueSql("('0')");

                entity.HasOne(d => d.FoodCategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasPrincipalKey(p => p.CategoryName)
                    .HasForeignKey(d => d.FoodCategory)
                    .HasConstraintName("FK_PRODUCTS_FCATEGORY");

                entity.HasOne(d => d.IdAddedNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdAdded)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRODUCTS_USERS");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(e => e.DayCalories)
                    .HasColumnType("decimal(7, 2)")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.DayCarbohydrates)
                    .HasColumnType("decimal(7, 2)")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.DayFats)
                    .HasColumnType("decimal(7, 2)")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.DayProteins)
                    .HasColumnType("decimal(7, 2)")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.EatPeriod)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.MostCategory).HasMaxLength(50);

                entity.Property(e => e.ReportDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdReportNavigation)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.IdReport)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_REPORTS_USERS");

                entity.HasOne(d => d.MostCategoryNavigation)
                    .WithMany(p => p.Reports)
                    .HasPrincipalKey(p => p.CategoryName)
                    .HasForeignKey(d => d.MostCategory)
                    .HasConstraintName("FK_REPORTS_FCATEGORY");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.UserPassword, "UQ__Users__5DF58B81DFA38FB1")
                    .IsUnique();

                entity.HasIndex(e => e.UserLogin, "UQ__Users__7F8E8D5EE5C60A53")
                    .IsUnique();

                entity.Property(e => e.IsAdmin)
                    .IsRequired()
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserLogin)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<UsersDatum>(entity =>
            {
                entity.Property(e => e.Age).HasDefaultValueSql("('0')");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.HasOne(d => d.IdDataNavigation)
                    .WithMany(p => p.UsersData)
                    .HasForeignKey(d => d.IdData)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USERS_DATA_USERS");
            });

            modelBuilder.Entity<UsersParam>(entity =>
            {
                entity.Property(e => e.ParamsDate).HasColumnType("datetime");

                entity.Property(e => e.UserHeight).HasDefaultValueSql("('0')");

                entity.Property(e => e.UserWeight)
                    .HasColumnType("decimal(4, 1)")
                    .HasDefaultValueSql("('0')");

                entity.HasOne(d => d.IdParamsNavigation)
                    .WithMany(p => p.UsersParams)
                    .HasForeignKey(d => d.IdParams)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USERS_PARAMS_USERS");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
