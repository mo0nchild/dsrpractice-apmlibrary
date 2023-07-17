﻿// <auto-generated />
using System;
using APMLibrary.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace APMLibrary.Dal.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    [Migration("20230711022516_UpdateDb6")]
    partial class UpdateDb6
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("APMLibrary.Dal.Entities.Authorization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<short>("AccountType")
                        .HasColumnType("smallint");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("ProfileId")
                        .IsUnique();

                    b.ToTable("Authorizations");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.BookCover", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("BackCover")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("FrontCover")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("BookCovers");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("bytea");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<Guid>("Reference")
                        .HasColumnType("uuid");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.Publication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<byte[]>("Body")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<int>("BookCoverId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<int>("NumberPages")
                        .HasColumnType("integer");

                    b.Property<int>("PublicationTypeId")
                        .HasColumnType("integer");

                    b.Property<bool>("Published")
                        .HasColumnType("boolean");

                    b.Property<int>("PublisherId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateOnly>("YearIssue")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("BookCoverId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("LanguageId");

                    b.HasIndex("PublicationTypeId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.PublicationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("PublicationTypes");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("PublicationId")
                        .HasColumnType("integer");

                    b.Property<int>("ReaderId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PublicationId");

                    b.HasIndex("ReaderId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOnly")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PublicationId")
                        .HasColumnType("integer");

                    b.Property<int>("ReaderId")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PublicationId");

                    b.HasIndex("ReaderId");

                    b.ToTable("Ratings", t =>
                        {
                            t.HasCheckConstraint("Value_CheckConstraint", "\"Value\" BETWEEN 0 AND 5");
                        });
                });

            modelBuilder.Entity("Bookmarks", b =>
                {
                    b.Property<int>("BookmarksId")
                        .HasColumnType("integer");

                    b.Property<int>("ReadersId")
                        .HasColumnType("integer");

                    b.HasKey("BookmarksId", "ReadersId");

                    b.HasIndex("ReadersId");

                    b.ToTable("Bookmarks");
                });

            modelBuilder.Entity("GenrePublication", b =>
                {
                    b.Property<int>("GenresId")
                        .HasColumnType("integer");

                    b.Property<int>("PublicationsId")
                        .HasColumnType("integer");

                    b.HasKey("GenresId", "PublicationsId");

                    b.HasIndex("PublicationsId");

                    b.ToTable("GenrePublication");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.Authorization", b =>
                {
                    b.HasOne("APMLibrary.Dal.Entities.Profile", "Profile")
                        .WithOne("Authorization")
                        .HasForeignKey("APMLibrary.Dal.Entities.Authorization", "ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.Genre", b =>
                {
                    b.HasOne("APMLibrary.Dal.Entities.Category", "Category")
                        .WithMany("Genres")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.Publication", b =>
                {
                    b.HasOne("APMLibrary.Dal.Entities.BookCover", "BookCover")
                        .WithMany("Publications")
                        .HasForeignKey("BookCoverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APMLibrary.Dal.Entities.Language", "Language")
                        .WithMany("Publications")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APMLibrary.Dal.Entities.PublicationType", "PublicationType")
                        .WithMany("Publications")
                        .HasForeignKey("PublicationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APMLibrary.Dal.Entities.Profile", "Publisher")
                        .WithMany("Publications")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookCover");

                    b.Navigation("Language");

                    b.Navigation("PublicationType");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.Quote", b =>
                {
                    b.HasOne("APMLibrary.Dal.Entities.Publication", "Publication")
                        .WithMany("Quotes")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APMLibrary.Dal.Entities.Profile", "Reader")
                        .WithMany("Quotes")
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publication");

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.Rating", b =>
                {
                    b.HasOne("APMLibrary.Dal.Entities.Publication", "Publication")
                        .WithMany("Ratings")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APMLibrary.Dal.Entities.Profile", "Reader")
                        .WithMany("Ratings")
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publication");

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("Bookmarks", b =>
                {
                    b.HasOne("APMLibrary.Dal.Entities.Publication", null)
                        .WithMany()
                        .HasForeignKey("BookmarksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APMLibrary.Dal.Entities.Profile", null)
                        .WithMany()
                        .HasForeignKey("ReadersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenrePublication", b =>
                {
                    b.HasOne("APMLibrary.Dal.Entities.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APMLibrary.Dal.Entities.Publication", null)
                        .WithMany()
                        .HasForeignKey("PublicationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.BookCover", b =>
                {
                    b.Navigation("Publications");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.Category", b =>
                {
                    b.Navigation("Genres");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.Language", b =>
                {
                    b.Navigation("Publications");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.Profile", b =>
                {
                    b.Navigation("Authorization")
                        .IsRequired();

                    b.Navigation("Publications");

                    b.Navigation("Quotes");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.Publication", b =>
                {
                    b.Navigation("Quotes");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("APMLibrary.Dal.Entities.PublicationType", b =>
                {
                    b.Navigation("Publications");
                });
#pragma warning restore 612, 618
        }
    }
}
