using System;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.Mappings
{
    public class CommentMapping
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .Property(x => x.Message)
                .HasColumnType("nvarchar")
                .HasMaxLength(512)
                .IsRequired();
        }
    }
}

