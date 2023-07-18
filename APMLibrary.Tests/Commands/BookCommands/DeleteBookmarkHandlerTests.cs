using APMLibrary.Bll.Commands.BookCommands.Handler;
using APMLibrary.Bll.Commands.BookCommands.Validations;
using APMLibrary.Bll.Commands.BookCommands;
using APMLibrary.Bll.Services.Interfaces;
using APMLibrary.Dal;
using APMLibrary.Tests.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APMLibrary.Bll.Commands.BookCommands.Handlers;

namespace APMLibrary.Tests.Commands.BookCommands
{
    [Collection("RequestsCollection")]
    public sealed class DeleteBookmarkHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        public DeleteBookmarkHandlerTests(RequestsTestFixture fixture) : base()
        {
            this.dbcontextFactory = fixture.Factory;
        }

        [Fact(DisplayName = "Удаление закладки на публикацию")]
        public async Task DeleteBookmarkHandler_Success()
        {
            var handler = new DeleteBookmarkHandler(dbcontextFactory);
            var request = new DeleteBookmarkCommand() { BookId = 1, ReaderId = 2 };

            await handler.Handle(request, CancellationToken.None);
            using (var context = await this.dbcontextFactory.CreateDbContextAsync())
            {
                var profile = await context.Profiles.Include(item => item.Bookmarks)
                    .FirstOrDefaultAsync(item => item.Id == 2);

                Assert.NotNull(profile);
                Assert.Null(profile?.Bookmarks.FirstOrDefault(item => item.Id == 1));
            }
        }

    }
}
