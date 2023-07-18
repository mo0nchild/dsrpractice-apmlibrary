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

namespace APMLibrary.Tests.Requests.ProfileRequests
{
    [Collection("RequestsCollection")]
    public sealed class GetProfileHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        private readonly IMapper mapper = default!;
        public GetProfileHandlerTests(RequestsTestFixture fixture) : base()
        {
            this.mapper = fixture.Mapper;
            this.dbcontextFactory = fixture.Factory;
        }

        [Fact(DisplayName = "Получение данных пользователя")]
        public async Task GetProfileHandler_Success()
        {
            var handler = new GetProfileHandler(dbcontextFactory, mapper);
            var request = new GetProfileRequest(1);

            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Профиль пользователя не найден")]
        public async Task GetProfileHandler_NotFound()
        {
            var handler = new GetProfileHandler(dbcontextFactory, mapper);
            var request = new GetProfileRequest(100);

            var result = await handler.Handle(request, CancellationToken.None);
            Assert.Null(result);
        }
    }
}
