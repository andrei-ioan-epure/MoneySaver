using System;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.Mappings
{
	public class UserMapping
	{
		public static void Map(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<User>()
				.Property(x => x.Email)
				.HasColumnType("nvarchar")
				.HasMaxLength(100)
				.IsRequired();

            modelBuilder.Entity<User>()
				.Property(x => x.Password)
				.HasColumnType("nvarchar")
				.HasMaxLength(100)
				.IsRequired();

			modelBuilder.Entity<User>()
				.Property(x => x.UserName)
				.HasColumnType("nvarchar")
				.HasMaxLength(100)
				.IsRequired();

			modelBuilder.Entity<User>()
				.Property(x => x.FullName)
				.HasColumnType("nvarchar")
				.HasMaxLength(100)
				.IsRequired();
        }
	}
}