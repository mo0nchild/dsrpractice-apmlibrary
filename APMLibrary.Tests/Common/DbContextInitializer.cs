using APMLibrary.Bll.Commands.BookCommands;
using APMLibrary.Dal;
using APMLibrary.Dal.Common;
using APMLibrary.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Tests.Common
{
    using BCrypt = BCrypt.Net.BCrypt;
    public static class DbContextInitializer : object
    {
        public static async Task Initialize(IDbContextFactory<LibraryDbContext> factory)
        {
            using var context = await factory.CreateDbContextAsync();

            await context.Database.EnsureCreatedAsync();
            await context.Languages.AddRangeAsync(
                new Language()
                {
                    Id = 1,
                    Name = "Русский",
                }
            );
            await context.PublicationTypes.AddRangeAsync(
                new PublicationType()
                {
                    Id = 1,
                    Name = "Методичка"
                }
            );
            await context.Categories.AddRangeAsync(
                new Category()
                {
                    Id = 1,
                    Name = "Научная литература"
                }
            );
            await context.Genres.AddRangeAsync(
                new Genre()
                {
                    Id = 1,
                    Name = "История",
                    CategoryId = 1
                }
            );
            await context.Profiles.AddRangeAsync(
                new Profile()
                {
                    Id = 1,
                    Email = "testuser1@gmail.com",
                    Phone = "+79001112233",
                    Name = "Крутой",
                    Surname = "Омлет",
                    Reference = Guid.NewGuid(),
                    Authorization = new Authorization()
                    {
                        Id = 1,
                        Login = "user1",
                        Password = BCrypt.HashPassword("1234567890"),
                    },
                },
                new Profile()
                {
                    Id = 2,
                    Email = "testuser2@gmail.com",
                    Phone = "+79009009090",
                    Name = "username2",
                    Surname = "surname2",
                    Reference = Guid.NewGuid(),
                    Authorization = new Authorization()
                    {
                        Id = 2,
                        Login = "testuser2",
                        Password = BCrypt.HashPassword("1234567890"),
                    },
                },
                new Profile()
                {
                    Id = 3,
                    Email = "testuser3@gmail.com",
                    Phone = "+74441112211",
                    Name = "username3",
                    Surname = "surname3",
                    Reference = Guid.NewGuid(),
                    Authorization = new Authorization()
                    {
                        Id = 3,
                        Login = "testuser3",
                        Password = BCrypt.HashPassword("1234567890"),
                    },
                }
            );
            await context.SaveChangesAsync();
            await context.Publications.AddRangeAsync(
                new Publication()
                {
                    Id = 1,
                    AuthorName = "Антон Чехов",
                    Title = "Вишневый Сад",
                    Body = new byte[10],
                    Description = "Description",
                    YearIssue = DateOnly.FromDateTime(DateTime.Now),
                    PublicationTypeId = 1,
                    LanguageId = 1,
                    NumberPages = 10,
                    Published = true,
                    Publisher = new Profile()
                    {
                        Id = 4,
                        Email = "publichser@gmail.com",
                        Phone = "+74441112211",
                        Name = "Publisher",
                        Surname = "surname4",
                        Reference = Guid.NewGuid(),
                        Authorization = new Authorization()
                        {
                            Id = 4,
                            Login = "publisher",
                            Password = BCrypt.HashPassword("1234567890"),
                        },
                    },
                    Genres = new List<Genre>()
                    {
                        new Genre()
                        {
                            CategoryId = 1,
                            Id = 10,
                            Name = "Спорт и здоровье",
                        }
                    },
                    PublisherId = 1,
                    Ratings = new List<Rating>()
                    {
                        new Rating()
                        {
                            Id = 1,
                            Comment = "Комментарий",
                            DateOnly = DateTime.Now,
                            ReaderId = 3,
                            Value = 4,
                        }
                    }
                }
            );
            await context.SaveChangesAsync();

            var profile = await context.Profiles.Include(item => item.Bookmarks)
                .FirstOrDefaultAsync(item => item.Id == 2);
            var book = await context.Publications.FirstOrDefaultAsync(item => item.Id == 1);

            if (profile != null && book != null)
            {
                profile.Bookmarks.Add(book);
                await context.SaveChangesAsync();
            }
        }
        public async static Task DestroyAsync(IDbContextFactory<LibraryDbContext> factory)
        {
            using var context = await factory.CreateDbContextAsync();
            await context.Database.EnsureDeletedAsync();
        }
    }
}
