using APMLibrary.Bll.Commands.BookCommands.Handler;
using APMLibrary.Bll.Commands.BookCommands.Validations;
using APMLibrary.Bll.Commands.BookCommands;
using APMLibrary.Bll.Services.Interfaces;
using APMLibrary.Dal;
using APMLibrary.Tests.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APMLibrary.Bll.Common.Exceptions;

namespace APMLibrary.Tests.Commands.BookCommands
{
    [Collection("RequestsCollection")]
    public sealed class UpdateBookHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        private readonly IMapper mapper = default!;
        public UpdateBookHandlerTests(RequestsTestFixture fixture) : base()
        {
            this.mapper = fixture.Mapper;
            this.dbcontextFactory = fixture.Factory;
        }

        [Fact(DisplayName = "Обновление публикации")]
        public async Task UpdateBookHandler_Success()
        {
            var handler = new UpdateBookHandler(dbcontextFactory, mapper);
            var request = new UpdateBookCommand()
            {
                BookId = 1,
                Title = "Хорошая книга",
                AuthorName = "Мега автор",
                Description = "Тест",
                Genres = new() { "История" },
                Language = "Русский",
                PublishType = "Методичка",
                YearIssue = DateOnly.FromDateTime(DateTime.Now),
            };
            Assert.True((await new UpdateBookValidation().ValidateAsync(request)).IsValid);
            await handler.Handle(request, CancellationToken.None);

            using (var context = await this.dbcontextFactory.CreateDbContextAsync())
            {
                var book = await context.Publications.Include(item => item.Language)
                    .Include(item => item.PublicationType).Include(item => item.Genres)
                    .FirstOrDefaultAsync(item => item.Id == 1);

                Assert.Equal("Мега автор", book?.AuthorName);
                Assert.Equal("Хорошая книга", book?.Title);

                Assert.Equal("Тест", book?.Description);
                Assert.Equal("Русский", book?.Language.Name);
                Assert.Equal("Методичка", book?.PublicationType.Name);

                Assert.True(book?.Genres.Select(item => item.Name).Contains("История"));
            }
        }

        [Fact(DisplayName = "Язык перевода не найден")]
        public async Task UpdateBookHandler_LanguageNotFound()
        {
            var handler = new UpdateBookHandler(dbcontextFactory, mapper);
            var request = new UpdateBookCommand()
            {
                BookId = 1,
                Title = "Хорошая книга",
                AuthorName = "Мега автор",
                Description = "Тест",
                Genres = new() { "История" },
                Language = "...",
                PublishType = "Методичка",
                YearIssue = DateOnly.FromDateTime(DateTime.Now),
            };
            Assert.True((await new UpdateBookValidation().ValidateAsync(request)).IsValid);
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }

        [Fact(DisplayName = "Публикации не найдена")]
        public async Task UpdateBookHandler_BookNotFound()
        {
            var handler = new UpdateBookHandler(dbcontextFactory, mapper);
            var request = new UpdateBookCommand()
            {
                BookId = 10,
                Title = "Хорошая книга",
                AuthorName = "Мега автор",
                Description = "Тест",
                Genres = new() { "История" },
                Language = "Русский",
                PublishType = "Методичка",
                YearIssue = DateOnly.FromDateTime(DateTime.Now),
            };
            Assert.True((await new UpdateBookValidation().ValidateAsync(request)).IsValid);
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }

        [Fact(DisplayName = "Тип публикации не найден")]
        public async Task UpdateBookHandler_BookTypeNotFound()
        {
            var handler = new UpdateBookHandler(dbcontextFactory, mapper);
            var request = new UpdateBookCommand()
            {
                BookId = 1,
                Title = "Хорошая книга",
                AuthorName = "Мега автор",
                Description = "Тест",
                Genres = new() { "История" },
                Language = "Русский",
                PublishType = "...",
                YearIssue = DateOnly.FromDateTime(DateTime.Now),
            };
            Assert.True((await new UpdateBookValidation().ValidateAsync(request)).IsValid);
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }

        [Fact(DisplayName = "Не установлен список жанров и категорий")]
        public async Task UpdateBookHandler_GenreNotFound()
        {
            var handler = new UpdateBookHandler(dbcontextFactory, mapper);
            var request = new UpdateBookCommand()
            {
                BookId = 1,
                Title = "Хорошая книга",
                AuthorName = "Мега автор",
                Description = "Тест",
                Genres = new() { "..." },
                Language = "Русский",
                PublishType = "Методичка",
                YearIssue = DateOnly.FromDateTime(DateTime.Now),
            };
            Assert.True((await new UpdateBookValidation().ValidateAsync(request)).IsValid);
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }
    }
}
