using APMLibrary.Bll.Commands.ProfileCommands.Handlers;
using APMLibrary.Bll.Commands.ProfileCommands.Validations;
using APMLibrary.Bll.Commands.ProfileCommands;
using APMLibrary.Dal;
using APMLibrary.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APMLibrary.Bll.Commands.BookCommands.Handler;
using AutoMapper;
using Moq;
using APMLibrary.Bll.Services.Interfaces;
using APMLibrary.Bll.Commands.BookCommands;
using APMLibrary.Bll.Commands.BookCommands.Validations;
using APMLibrary.Bll.Common.Exceptions;

namespace APMLibrary.Tests.Commands.BookCommands
{
    [Collection("RequestsCollection")]
    public sealed class CreateBookHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        private readonly IMapper _mapper = default!;

        public CreateBookHandlerTests(RequestsTestFixture fixture) : base()
        {
            this._mapper = fixture.Mapper;
            this._dbcontextFactory = fixture.Factory;
        }

        [Fact(DisplayName = "Создание публикации")]
        public async Task CreateBookHandler_Success()
        {
            var textReaderMock = Mock.Of<ITextFileReader>(item => item.GetPagesCount(It.IsAny<byte[]>()) == 0);
            var handler = new CreateBookHandler(_dbcontextFactory, _mapper, textReaderMock);
            var request = new CreateBookCommand()
            {
                AuthorName = "Антон Чехов",
                Title = "Вишневый Сад",
                BookBody = new byte[10],
                Description = "Description",
                Language = "Русский", 
                YearIssue = DateOnly.FromDateTime(DateTime.Now),
                PublishType = "Методичка",
                Genres = new() { "История" },
                PublisherId = 1,
            };
            Assert.True((await new CreateBookValidation().ValidateAsync(request)).IsValid);
            var result = await handler.Handle(request, CancellationToken.None);

            using (var context = await _dbcontextFactory.CreateDbContextAsync())
            {
                var book = await context.Publications.Include(item => item.Language)
                    .Include(item => item.PublicationType).Include(item => item.Genres)
                    .FirstOrDefaultAsync(item => item.Id == result);

                Assert.Equal("Антон Чехов", book?.AuthorName);
                Assert.Equal("Вишневый Сад", book?.Title);

                Assert.Equal("Description", book?.Description);
                Assert.Equal("Русский", book?.Language.Name);
                Assert.Equal("Методичка", book?.PublicationType.Name);

                Assert.True(book?.Genres.Select(item => item.Name).Contains("История"));
            }
        }

        [Fact(DisplayName = "Профиль издателя не найден")]
        public async Task CreateBookHandler_PublisherNotFound()
        {
            var textReaderMock = Mock.Of<ITextFileReader>(item => item.GetPagesCount(It.IsAny<byte[]>()) == 0);
            var handler = new CreateBookHandler(_dbcontextFactory, _mapper, textReaderMock);
            var request = new CreateBookCommand()
            {
                AuthorName = "Антон Чехов",
                Title = "Вишневый Сад",
                BookBody = new byte[10],
                Description = "Description",
                Language = "Русский",
                YearIssue = DateOnly.FromDateTime(DateTime.Now),
                PublishType = "Методичка",
                Genres = new() { "История" },
                PublisherId = 10,
            };
            Assert.True((await new CreateBookValidation().ValidateAsync(request)).IsValid);
            await Assert.ThrowsAsync<CreateBookException>(async () =>
            {
                var result = await handler.Handle(request, CancellationToken.None);
            });
        }

        [Fact(DisplayName = "Язык перевода не найден")]
        public async Task CreateBookHandler_LanguageNotFound()
        {
            var textReaderMock = Mock.Of<ITextFileReader>(item => item.GetPagesCount(It.IsAny<byte[]>()) == 0);
            var handler = new CreateBookHandler(_dbcontextFactory, _mapper, textReaderMock);
            var request = new CreateBookCommand()
            {
                AuthorName = "Антон Чехов",
                Title = "Вишневый Сад",
                BookBody = new byte[10],
                Description = "Description",
                Language = "Такого языка нет",
                YearIssue = DateOnly.FromDateTime(DateTime.Now),
                PublishType = "Методичка",
                Genres = new() { "История" },
                PublisherId = 1,
            };
            Assert.True((await new CreateBookValidation().ValidateAsync(request)).IsValid);
            await Assert.ThrowsAsync<CreateBookException>(async () =>
            {
                var result = await handler.Handle(request, CancellationToken.None);
            });
        }

        [Fact(DisplayName = "Тип публикации не найден")]
        public async Task CreateBookHandler_BookTypeNotFound()
        {
            var textReaderMock = Mock.Of<ITextFileReader>(item => item.GetPagesCount(It.IsAny<byte[]>()) == 0);
            var handler = new CreateBookHandler(_dbcontextFactory, _mapper, textReaderMock);
            var request = new CreateBookCommand()
            {
                AuthorName = "Антон Чехов",
                Title = "Вишневый Сад",
                BookBody = new byte[10],
                Description = "Description",
                Language = "Русский",
                YearIssue = DateOnly.FromDateTime(DateTime.Now),
                PublishType = "...",
                Genres = new() { "История" },
                PublisherId = 1,
            };
            Assert.True((await new CreateBookValidation().ValidateAsync(request)).IsValid);
            await Assert.ThrowsAsync<CreateBookException>(async () =>
            {
                var result = await handler.Handle(request, CancellationToken.None);
            });
        }

        [Fact(DisplayName = "Не установлен список жанров и категорий")]
        public async Task CreateBookHandler_GenreNotFound()
        {
            var textReaderMock = Mock.Of<ITextFileReader>(item => item.GetPagesCount(It.IsAny<byte[]>()) == 0);
            var handler = new CreateBookHandler(_dbcontextFactory, _mapper, textReaderMock);
            var request = new CreateBookCommand()
            {
                AuthorName = "Антон Чехов",
                Title = "Вишневый Сад",
                BookBody = new byte[10],
                Description = "Description",
                Language = "Русский",
                YearIssue = DateOnly.FromDateTime(DateTime.Now),
                PublishType = "Методичка",
                Genres = new() { "...", "..." },
                PublisherId = 1,
            };
            Assert.True((await new CreateBookValidation().ValidateAsync(request)).IsValid);
            await Assert.ThrowsAsync<CreateBookException>(async () =>
            {
                var result = await handler.Handle(request, CancellationToken.None);
            });
        }
    }
}
