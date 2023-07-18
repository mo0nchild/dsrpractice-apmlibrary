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
using APMLibrary.Bll.Common.Exceptions;

namespace APMLibrary.Tests.Requests.BookRequests
{
    [Collection("RequestsCollection")]
    public sealed class GetRatingsHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        private readonly IMapper mapper = default!;
        public GetRatingsHandlerTests(RequestsTestFixture fixture) : base()
        {
            this.mapper = fixture.Mapper;
            this.dbcontextFactory = fixture.Factory;
        }

        [Fact(DisplayName = "Получение информации о комментариях")]
        public async Task GetRatingsHandler_Success()
        {
            var handler = new GetRatingsHandler(dbcontextFactory, mapper);
            var request = new GetRatingsRequest() { BookId = 1, Skip = 0, Take = 10 };

            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);

            Assert.True(result.Ratings.Count() > 0);
        }

        [Fact(DisplayName = "Публикация не найдена")]
        public async Task GetRatingsHandler_NotFound()
        {
            var handler = new GetRatingsHandler(dbcontextFactory, mapper);
            var request = new GetRatingsRequest() { BookId = 10, Skip = 0, Take = 10 };

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                var result = await handler.Handle(request, CancellationToken.None);
            });
        }
    }
}
