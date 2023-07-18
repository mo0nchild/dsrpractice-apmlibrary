using APMLibrary.Bll.Requests.ProfileRequests.Handlers;
using APMLibrary.Bll.Requests.ProfileRequests;
using APMLibrary.Dal;
using APMLibrary.Tests.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APMLibrary.Bll.Requests.BookRequests.Handlers;
using APMLibrary.Bll.Requests.BookRequests;

namespace APMLibrary.Tests.Requests.BookRequests
{
    [Collection("RequestsCollection")]
    public sealed class GetBookInfoHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        private readonly IMapper mapper = default!;
        public GetBookInfoHandlerTests(RequestsTestFixture fixture) : base()
        {
            this.mapper = fixture.Mapper;
            this.dbcontextFactory = fixture.Factory;
        }

        [Fact(DisplayName = "Получение информации о публикации")]
        public async Task GetBookInfoHandler_Success()
        {
            var handler = new GetBookHandler(dbcontextFactory, mapper);
            var request = new GetBookRequest(1);

            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Публикация не найдена")]
        public async Task GetBookInfoHandler_NotFound()
        {
            var handler = new GetBookHandler(dbcontextFactory, mapper);
            var request = new GetBookRequest(100);

            var result = await handler.Handle(request, CancellationToken.None);
            Assert.Null(result);
        }
    }
}
