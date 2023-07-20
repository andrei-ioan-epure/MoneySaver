using System;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.Mappings
{
	public class ArticleMapping
	{
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .Property(x => x.Title)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Article>()
                .Property(x => x.City)
                .HasColumnType("nvarchar")
                .HasMaxLength(24)
                .IsRequired();

            modelBuilder.Entity<Article>()
                .Property(x => x.Category)
                .HasColumnType("nvarchar")
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Article>()
                .Property(x => x.Code)
                .HasColumnType("nvarchar")
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Article>()
                .Property(x => x.Store)
                .HasColumnType("nvarchar")
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Article>()
                .Property(x => x.Author)
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Article>()
                .Property(x => x.Content)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();
        }
    }
}

