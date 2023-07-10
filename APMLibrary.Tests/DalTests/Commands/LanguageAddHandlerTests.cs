using APMLibrary.Bll.Commands.SupportCommand;
using APMLibrary.Bll.Commands.SupportCommand.Handlers;
using APMLibrary.Bll.Commands.SupportCommand.Validations;
using APMLibrary.Dal;
using APMLibrary.Tests.DalTests.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace APMLibrary.Tests.DalTests.Commands
{
    [Collection("RequestsCollection")]
    public sealed class LanguageAddHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        private readonly IMapper _mapper = default!;

        public LanguageAddHandlerTests(RequestsTestFixture fixture) : base() 
            => (this._dbcontextFactory, this._mapper) = (fixture.Factory, fixture.Mapper);

        [Fact(DisplayName = "Проверка добавления языка перевода")]
        public async Task LanguageAddHandler_Success()
        {
            var handler = new LanguageAddHandler(this._dbcontextFactory,  this._mapper);
            var request = new LanguageAddCommand() { Name = "Английский" };

            Assert.True((await new LanguageAddValidation().ValidateAsync(request)).IsValid);
            await handler.Handle(request, CancellationToken.None);

            using (var context = await this._dbcontextFactory.CreateDbContextAsync())
            {
                Assert.NotNull(await context.Languages
                    .SingleOrDefaultAsync(item => item.Name == "Английский"));
            }
        }
    }
}
