﻿// <auto-generated />
using System;
using DomainLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DomainLayer.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    [Migration("20230731130029_CreateDB")]
    partial class CreateDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArticleUser", b =>
                {
                    b.Property<int>("FavoriteArticlesId")
                        .HasColumnType("int");

                    b.Property<int>("FavoriteUsersId")
                        .HasColumnType("int");

                    b.HasKey("FavoriteArticlesId", "FavoriteUsersId");

                    b.HasIndex("FavoriteUsersId");

                    b.ToTable("ArticleUser");
                });

            modelBuilder.Entity("CommentUser", b =>
                {
                    b.Property<int>("LikedById")
                        .HasColumnType("int");

                    b.Property<int>("LikedCommentsId")
                        .HasColumnType("int");

                    b.HasKey("LikedById", "LikedCommentsId");

                    b.HasIndex("LikedCommentsId");

                    b.ToTable("CommentUser");
                });

            modelBuilder.Entity("DomainLayer.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Posted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Store")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("DomainLayer.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("CreatorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar");

                    b.Property<DateTime>("Posted")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DomainLayer.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar");

                    b.Property<bool>("IsCreator")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ArticleUser", b =>
                {
                    b.HasOne("DomainLayer.Models.Article", null)
                        .WithMany()
                        .HasForeignKey("FavoriteArticlesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainLayer.Models.User", null)
                        .WithMany()
                        .HasForeignKey("FavoriteUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CommentUser", b =>
                {
                    b.HasOne("DomainLayer.Models.User", null)
                        .WithMany()
                        .HasForeignKey("LikedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainLayer.Models.Comment", null)
                        .WithMany()
                        .HasForeignKey("LikedCommentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DomainLayer.Models.Article", b =>
                {
                    b.HasOne("DomainLayer.Models.User", "Creator")
                        .WithMany("CreatedArticles")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("DomainLayer.Models.Comment", b =>
                {
                    b.HasOne("DomainLayer.Models.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DomainLayer.Models.User", "Creator")
                        .WithMany("Comments")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.Navigation("Article");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("DomainLayer.Models.Article", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("DomainLayer.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("CreatedArticles");
                });
#pragma warning restore 612, 618
        }
    }
}
