using APMLibrary.Bll.Commands.SupportCommand;
using APMLibrary.Bll.Commands.SupportCommand.Handlers;
using APMLibrary.Bll.Commands.SupportCommand.Validations;
using APMLibrary.Bll.Common.Exceptions;
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
using Xunit;

namespace APMLibrary.Tests.Commands.SupportCommands
{
    [Collection("RequestsCollection")]
    public sealed class LanguageAddHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        private readonly IMapper _mapper = default!;

        public LanguageAddHandlerTests(RequestsTestFixture fixture) : base()
            => (_dbcontextFactory, _mapper) = (fixture.Factory, fixture.Mapper);

        [Fact(DisplayName = "Проверка добавления языка перевода")]
        public async Task LanguageAddHandler_Success()
        {
            var handler = new LanguageAddHandler(_dbcontextFactory, _mapper);
            var request = new LanguageAddCommand() { Name = "Английский" };

            Assert.True((await new LanguageAddValidation().ValidateAsync(request)).IsValid);
            await handler.Handle(request, CancellationToken.None);

            using (var context = await _dbcontextFactory.CreateDbContextAsync())
            {
                Assert.NotNull(await context.Languages
                    .SingleOrDefaultAsync(item => item.Name == "Английский"));
            }
        }

        [Fact(DisplayName = "Язык публикации уже добавлен")]
        public async Task LanguageAddHandler_Collision()
        {
            var handler = new LanguageAddHandler(_dbcontextFactory, _mapper);
            var request = new LanguageAddCommand() { Name = "Русский" };

            Assert.True((await new LanguageAddValidation().ValidateAsync(request)).IsValid);
            await Assert.ThrowsAsync<CollisionException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }
    }
}
