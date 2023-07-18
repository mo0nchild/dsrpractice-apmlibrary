using APMLibrary.Bll.Commands.SupportCommand.Handlers;
using APMLibrary.Bll.Commands.SupportCommand.Validations;
using APMLibrary.Bll.Commands.SupportCommand;
using APMLibrary.Dal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Tests.Common;

namespace APMLibrary.Tests.Commands.SupportCommands
{
    [Collection("RequestsCollection")]
    public sealed class GenreAddHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        private readonly IMapper _mapper = default!;

        public GenreAddHandlerTests(RequestsTestFixture fixture) : base()
        {
            _dbcontextFactory = fixture.Factory;
            _mapper = fixture.Mapper;
        }

        [Fact(DisplayName = "Добавление жанра публикации")]
        public async Task GenreAddHandler_Success()
        {
            var handler = new GenreAddHandler(_dbcontextFactory, _mapper);
            var request = new GenreAddCommand() { Name = "Фантастика", Category = "Научная литература" };

            Assert.True((await new GenreAddValidation().ValidateAsync(request)).IsValid);
            await handler.Handle(request, CancellationToken.None);

            using (var context = await _dbcontextFactory.CreateDbContextAsync())
            {
                Assert.NotNull(await context.Genres
                    .SingleOrDefaultAsync(item => item.Name == "Фантастика"));
            }
        }

        [Fact(DisplayName = "Жанр публикации уже добавлен")]
        public async Task GenreAddHandler_Collision()
        {
            var handler = new GenreAddHandler(_dbcontextFactory, _mapper);
            var request = new GenreAddCommand() { Name = "История", Category = "Научная литература" };

            Assert.True((await new GenreAddValidation().ValidateAsync(request)).IsValid);
            await Assert.ThrowsAsync<CollisionException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }

        [Fact(DisplayName = "Категория публикации не найдена")]
        public async Task GenreAddHandler_NotFound()
        {
            var handler = new GenreAddHandler(_dbcontextFactory, _mapper);
            var request = new GenreAddCommand() { Name = "Фантастика", Category = "Такой нету хихи" };

            Assert.True((await new GenreAddValidation().ValidateAsync(request)).IsValid);
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }
    }
}
