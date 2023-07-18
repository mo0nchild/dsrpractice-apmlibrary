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
using APMLibrary.Bll.Requests.ProfileRequests.Handlers;
using APMLibrary.Bll.Requests.ProfileRequests;

namespace APMLibrary.Tests.Requests.ProfileRequests
{
    [Collection("RequestsCollection")]
    public sealed class GetAuthorizationHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        private readonly IMapper mapper = default!;
        public GetAuthorizationHandlerTests(RequestsTestFixture fixture) : base()
        {
            this.mapper = fixture.Mapper;
            this.dbcontextFactory = fixture.Factory;
        }

        [Fact(DisplayName = "Авторизация пользователя в систему")]
        public async Task GetAuthorizationHandler_Success()
        {
            var handler = new GetAuthorizationHandler(dbcontextFactory, mapper);
            var request = new GetAuthorizationRequest() { Login = "testuser3", Password = "1234567890" };

            var result = await handler.Handle(request, CancellationToken.None);
            Assert.NotNull(result);
        }

        [Fact(DisplayName = "Введены неправильные данные")]
        public async Task GetAuthorizationHandler_WrongData()
        {
            var handler = new GetAuthorizationHandler(dbcontextFactory, mapper);
            var request = new GetAuthorizationRequest() { Login = "testuser14", Password = "..." };

            var result = await handler.Handle(request, CancellationToken.None);
            Assert.Null(result);
        }
    }
}
