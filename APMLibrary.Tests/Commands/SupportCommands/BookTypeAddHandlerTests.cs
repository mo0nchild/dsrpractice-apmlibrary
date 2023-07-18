using APMLibrary.Bll.Commands.SupportCommand.Handlers;
using APMLibrary.Bll.Commands.SupportCommand.Validations;
using APMLibrary.Bll.Commands.SupportCommand;
using APMLibrary.Dal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Tests.Common;

namespace APMLibrary.Tests.Commands.SupportCommands
{
    [Collection("RequestsCollection")]
    public sealed class BookTypeAddHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        private readonly IMapper _mapper = default!;

        public BookTypeAddHandlerTests(RequestsTestFixture fixture) : base()
        {
            _dbcontextFactory = fixture.Factory;
            _mapper = fixture.Mapper;
        }

        [Fact(DisplayName = "Добавление типа публикации")]
        public async Task BookTypeAddHandler_Success()
        {
            var handler = new BookTypeAddHandler(_dbcontextFactory, _mapper);
            var request = new BookTypeAddCommand() { Name = "Книга" };

            Assert.True((await new BookTypeAddValidation().ValidateAsync(request)).IsValid);
            await handler.Handle(request, CancellationToken.None);

            using (var context = await _dbcontextFactory.CreateDbContextAsync())
            {
                Assert.NotNull(await context.PublicationTypes
                    .SingleOrDefaultAsync(item => item.Name == "Книга"));
            }
        }
        [Fact(DisplayName = "Тип публикации уже хранится")]
        public async Task BookTypeAddHandler_Collision()
        {
            var handler = new BookTypeAddHandler(_dbcontextFactory, _mapper);
            var request = new BookTypeAddCommand() { Name = "Методичка" };

            Assert.True((await new BookTypeAddValidation().ValidateAsync(request)).IsValid);
            await Assert.ThrowsAsync<CollisionException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });

        }
    }
}
