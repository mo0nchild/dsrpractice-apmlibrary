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
    public sealed class GetProfilesListHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        private readonly IMapper mapper = default!;
        public GetProfilesListHandlerTests(RequestsTestFixture fixture) : base()
        {
            this.mapper = fixture.Mapper;
            this.dbcontextFactory = fixture.Factory;
        }

        [Fact(DisplayName = "Получение списка пользователей")]
        public async Task GetProfilesListHandler_Success()
        {
            var handler = new GetProfilesListHandler(dbcontextFactory, mapper);
            var request = new GetProfilesListRequest() { Skip = 0, Take = 3 };

            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);
            Assert.Equal(3, result.Profiles.Count());
        }
    }
}
