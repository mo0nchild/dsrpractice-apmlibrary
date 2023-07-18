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
    public sealed class CategoryAddHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        private readonly IMapper _mapper = default!;

        public CategoryAddHandlerTests(RequestsTestFixture fixture) : base()
        {
            _dbcontextFactory = fixture.Factory;
            _mapper = fixture.Mapper;
        }

        [Fact(DisplayName = "Добавление категории публикации")]
        public async Task CategoryAddHandler_Success()
        {
            var handler = new CategoryAddHandler(_dbcontextFactory, _mapper);
            var request = new CategoryAddCommand() { Name = "Классическая литература" };

            Assert.True((await new CategoryAddValidation().ValidateAsync(request)).IsValid);
            await handler.Handle(request, CancellationToken.None);

            using (var context = await _dbcontextFactory.CreateDbContextAsync())
            {
                Assert.NotNull(await context.Categories
                    .SingleOrDefaultAsync(item => item.Name == "Классическая литература"));
            }
        }

        [Fact(DisplayName = "Категория публикации уже хранится")]
        public async Task CategoryAddHandler_Collision()
        {
            var handler = new CategoryAddHandler(_dbcontextFactory, _mapper);
            var request = new CategoryAddCommand() { Name = "Научная литература" };

            Assert.True((await new CategoryAddValidation().ValidateAsync(request)).IsValid);
            await Assert.ThrowsAsync<CollisionException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }
    }
}
