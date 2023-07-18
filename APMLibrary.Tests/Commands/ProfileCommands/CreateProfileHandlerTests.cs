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
using APMLibrary.Bll.Commands.ProfileCommands.Handlers;
using APMLibrary.Bll.Commands.ProfileCommands;
using APMLibrary.Bll.Commands.ProfileCommands.Validations;
using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Tests.Common;

namespace APMLibrary.Tests.Commands.ProfileCommands
{
    [Collection("RequestsCollection")]
    public sealed class CreateProfileHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        private readonly IMapper _mapper = default!;

        public CreateProfileHandlerTests(RequestsTestFixture fixture) : base()
        {
            _dbcontextFactory = fixture.Factory;
            _mapper = fixture.Mapper;
        }

        [Fact(DisplayName = "Создание профиля пользователя")]
        public async Task CreateProfileHandler_Success()
        {
            var handler = new CreateProfileHandler(_dbcontextFactory, _mapper);
            var request = new CreateProfileCommand()
            {
                Login = "user2",
                Password = "123456789",
                Email = "usertest@gmail.com",
                Phone = "+79009099090",
                Surname = "Иван",
                UserName = "Иванович",
            };

            Assert.True((await new CreateProfileValidation().ValidateAsync(request)).IsValid);
            var result = await handler.Handle(request, CancellationToken.None);

            using (var context = await _dbcontextFactory.CreateDbContextAsync())
            {
                Assert.NotNull(await context.Profiles
                    .FirstOrDefaultAsync(item => item.Id == result));
            }
        }

        [Fact(DisplayName = "Логин занят другим пользователем")]
        public async Task CreateProfileHandler_Collision()
        {
            var handler = new CreateProfileHandler(_dbcontextFactory, _mapper);
            var request = new CreateProfileCommand()
            {
                Login = "user1",
                Password = "123456789",
                Email = "usertest@gmail.com",
                Phone = "+79009099090",
                Surname = "Иван",
                UserName = "Иванович",
            };
            Assert.True((await new CreateProfileValidation().ValidateAsync(request)).IsValid);
            await Assert.ThrowsAsync<CreateProfileException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }
    }
}
