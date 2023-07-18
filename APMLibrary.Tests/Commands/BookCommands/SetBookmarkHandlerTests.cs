using APMLibrary.Bll.Commands.BookCommands.Handler;
using APMLibrary.Bll.Commands.BookCommands.Validations;
using APMLibrary.Bll.Commands.BookCommands;
using APMLibrary.Dal;
using APMLibrary.Tests.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APMLibrary.Bll.Commands.BookCommands.Handlers;
using APMLibrary.Bll.Common.Exceptions;

namespace APMLibrary.Tests.Commands.BookCommands
{
    [Collection("RequestsCollection")]
    public sealed class SetBookmarkHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        private readonly IMapper mapper = default!;
        public SetBookmarkHandlerTests(RequestsTestFixture fixture) : base()
        {
            this.mapper = fixture.Mapper;
            this.dbcontextFactory = fixture.Factory;
        }

        [Fact(DisplayName = "Создание заметки")]
        public async Task SetBookmarkHandler_Success()
        {
            var handler = new SetBookmarkHandler(dbcontextFactory);
            var request = new SetBookmarkCommand() { PublishId = 1, ReaderId = 1 };

            await handler.Handle(request, CancellationToken.None);
            using (var context = await this.dbcontextFactory.CreateDbContextAsync())
            {
                var profile = await context.Profiles.Include(item => item.Bookmarks)
                    .FirstOrDefaultAsync(item => item.Id == 1);

                Assert.NotNull(profile);
                Assert.NotNull(profile.Bookmarks.FirstOrDefault(item => item.Id == 1));
            }
        }

        [Fact(DisplayName = "Профиль не найден")]
        public async Task SetBookmarkHandler_ProfileNotFound()
        {
            var handler = new SetBookmarkHandler(dbcontextFactory);
            var request = new SetBookmarkCommand() { PublishId = 1, ReaderId = 10 };

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }

        [Fact(DisplayName = "Публикация не найдена")]
        public async Task SetBookmarkHandler_BookNotFound()
        {
            var handler = new SetBookmarkHandler(dbcontextFactory);
            var request = new SetBookmarkCommand() { PublishId = 10, ReaderId = 1 };

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }
    }
}
