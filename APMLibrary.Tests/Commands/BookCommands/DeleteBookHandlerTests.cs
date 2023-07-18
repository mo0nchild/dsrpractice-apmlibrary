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
using APMLibrary.Bll.Common.Exceptions;

namespace APMLibrary.Tests.Commands.BookCommands
{
    [Collection("RequestsCollection")]
    public sealed class DeleteBookHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        public DeleteBookHandlerTests(RequestsTestFixture fixture) : base()
        {
            this._dbcontextFactory = fixture.Factory;
        }

        [Fact(DisplayName = "Удаление публикации")]
        public async Task DeleteBookHandler_Success()
        {
            var handler = new DeleteBookHandler(_dbcontextFactory);
            await handler.Handle(new DeleteBookCommand(1), CancellationToken.None);

            using (var context = await _dbcontextFactory.CreateDbContextAsync())
            {
                Assert.Null(await context.Publications.FirstOrDefaultAsync(item => item.Id == 1));
            }
        }

        [Fact(DisplayName = "Публикация не найдена")]
        public async Task DeleteBookHandler_NotFound()
        {
            var handler = new DeleteBookHandler(_dbcontextFactory);
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(new DeleteBookCommand(10), CancellationToken.None);
            });
        }
    }
}
