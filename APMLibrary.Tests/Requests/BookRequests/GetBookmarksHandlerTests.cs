using APMLibrary.Bll.Requests.BookRequests.Handlers;
using APMLibrary.Bll.Requests.BookRequests;
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

namespace APMLibrary.Tests.Requests.BookRequests
{
    [Collection("RequestsCollection")]
    public sealed class GetBookmarksHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        private readonly IMapper mapper = default!;
        public GetBookmarksHandlerTests(RequestsTestFixture fixture) : base()
        {
            this.mapper = fixture.Mapper;
            this.dbcontextFactory = fixture.Factory;
        }

        [Fact(DisplayName = "Получение списка заметок")]
        public async Task GetBookmarksHandler_Success()
        {
            var handler = new GetBookmarksHandler(dbcontextFactory, mapper);
            var request = new GetBookmarksRequest(2);

            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
        }

        [Fact(DisplayName = "Пользователь не найден")]
        public async Task GetBookmarksHandler_NotFound()
        {
            var handler = new GetBookmarksHandler(dbcontextFactory, mapper);
            var request = new GetBookmarksRequest(200);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                var result = await handler.Handle(request, CancellationToken.None);
            });
        }
    }
}
