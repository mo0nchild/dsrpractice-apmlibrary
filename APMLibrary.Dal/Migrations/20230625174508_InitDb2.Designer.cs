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
    [Migration("20230625174508_InitDb2")]
    partial class InitDb2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("APMLibraryDAL.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ProfileId")
                        .HasColumnType("integer");

                    b.Property<Guid>("Reference")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ProfileId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("APMLibraryDAL.Models.Authorization", b =>
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
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

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

            modelBuilder.Entity("APMLibraryDAL.Models.BookCover", b =>
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

            modelBuilder.Entity("APMLibraryDAL.Models.Category", b =>
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

            modelBuilder.Entity("APMLibraryDAL.Models.Genre", b =>
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

            modelBuilder.Entity("APMLibraryDAL.Models.Language", b =>
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

            modelBuilder.Entity("APMLibraryDAL.Models.Profile", b =>
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

                    b.Property<string>("Patronymic")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

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

            modelBuilder.Entity("APMLibraryDAL.Models.Publication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Body")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<int>("BookCoverId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("ForSubscriber")
                        .HasColumnType("boolean");

                    b.Property<int>("LanguageId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("NumberPages")
                        .HasColumnType("integer");

                    b.Property<int>("PublicationTypeId")
                        .HasColumnType("integer");

                    b.Property<bool>("Published")
                        .HasColumnType("boolean");

                    b.Property<string>("VendorCode")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<DateOnly>("YearIssue")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookCoverId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("LanguageId");

                    b.HasIndex("PublicationTypeId");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("APMLibraryDAL.Models.PublicationType", b =>
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

            modelBuilder.Entity("APMLibraryDAL.Models.Quote", b =>
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

            modelBuilder.Entity("APMLibraryDAL.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateOnly>("DateOnly")
                        .HasColumnType("date");

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

            modelBuilder.Entity("APMLibraryDAL.Models.Reader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ProfileId")
                        .HasColumnType("integer");

                    b.Property<Guid>("Reference")
                        .HasColumnType("uuid");

                    b.Property<bool>("Subscriber")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("ProfileId");

                    b.ToTable("Readers");
                });

            modelBuilder.Entity("Bookmarks", b =>
                {
                    b.Property<int>("PublicationsId")
                        .HasColumnType("integer");

                    b.Property<int>("ReadersId")
                        .HasColumnType("integer");

                    b.HasKey("PublicationsId", "ReadersId");

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

            modelBuilder.Entity("APMLibraryDAL.Models.Author", b =>
                {
                    b.HasOne("APMLibraryDAL.Models.Profile", "Profile")
                        .WithMany("Authors")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("APMLibraryDAL.Models.Authorization", b =>
                {
                    b.HasOne("APMLibraryDAL.Models.Profile", "Profile")
                        .WithOne("Authorization")
                        .HasForeignKey("APMLibraryDAL.Models.Authorization", "ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("APMLibraryDAL.Models.Genre", b =>
                {
                    b.HasOne("APMLibraryDAL.Models.Category", "Category")
                        .WithMany("Genres")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("APMLibraryDAL.Models.Publication", b =>
                {
                    b.HasOne("APMLibraryDAL.Models.Author", "Author")
                        .WithMany("Publications")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APMLibraryDAL.Models.BookCover", "BookCover")
                        .WithMany("Publications")
                        .HasForeignKey("BookCoverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APMLibraryDAL.Models.Language", "Language")
                        .WithMany("Publications")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APMLibraryDAL.Models.PublicationType", "PublicationType")
                        .WithMany("Publications")
                        .HasForeignKey("PublicationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("BookCover");

                    b.Navigation("Language");

                    b.Navigation("PublicationType");
                });

            modelBuilder.Entity("APMLibraryDAL.Models.Quote", b =>
                {
                    b.HasOne("APMLibraryDAL.Models.Publication", "Publication")
                        .WithMany("Quotes")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APMLibraryDAL.Models.Reader", "Reader")
                        .WithMany("Quotes")
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publication");

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("APMLibraryDAL.Models.Rating", b =>
                {
                    b.HasOne("APMLibraryDAL.Models.Publication", "Publication")
                        .WithMany("Ratings")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APMLibraryDAL.Models.Reader", "Reader")
                        .WithMany("Ratings")
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publication");

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("APMLibraryDAL.Models.Reader", b =>
                {
                    b.HasOne("APMLibraryDAL.Models.Profile", "Profile")
                        .WithMany("Readers")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Bookmarks", b =>
                {
                    b.HasOne("APMLibraryDAL.Models.Publication", null)
                        .WithMany()
                        .HasForeignKey("PublicationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APMLibraryDAL.Models.Reader", null)
                        .WithMany()
                        .HasForeignKey("ReadersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenrePublication", b =>
                {
                    b.HasOne("APMLibraryDAL.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APMLibraryDAL.Models.Publication", null)
                        .WithMany()
                        .HasForeignKey("PublicationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("APMLibraryDAL.Models.Author", b =>
                {
                    b.Navigation("Publications");
                });

            modelBuilder.Entity("APMLibraryDAL.Models.BookCover", b =>
                {
                    b.Navigation("Publications");
                });

            modelBuilder.Entity("APMLibraryDAL.Models.Category", b =>
                {
                    b.Navigation("Genres");
                });

            modelBuilder.Entity("APMLibraryDAL.Models.Language", b =>
                {
                    b.Navigation("Publications");
                });

            modelBuilder.Entity("APMLibraryDAL.Models.Profile", b =>
                {
                    b.Navigation("Authorization")
                        .IsRequired();

                    b.Navigation("Authors");

                    b.Navigation("Readers");
                });

            modelBuilder.Entity("APMLibraryDAL.Models.Publication", b =>
                {
                    b.Navigation("Quotes");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("APMLibraryDAL.Models.PublicationType", b =>
                {
                    b.Navigation("Publications");
                });

            modelBuilder.Entity("APMLibraryDAL.Models.Reader", b =>
                {
                    b.Navigation("Quotes");

                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}