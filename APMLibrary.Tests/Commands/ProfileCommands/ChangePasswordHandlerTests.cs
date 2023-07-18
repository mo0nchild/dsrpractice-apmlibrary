using APMLibrary.Bll.Commands.ProfileCommands.Handlers;
using APMLibrary.Bll.Commands.ProfileCommands.Validations;
using APMLibrary.Bll.Commands.ProfileCommands;
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

namespace APMLibrary.Tests.Commands.ProfileCommands
{
    using BCryptType = BCrypt.Net.BCrypt;

    [Collection("RequestsCollection")]
    public sealed class ChangePasswordHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        public ChangePasswordHandlerTests(RequestsTestFixture fixture) : base()
        {
            _dbcontextFactory = fixture.Factory;
        }

        [Fact(DisplayName = "Изменение пароль пользователя")]
        public async Task ChangePasswordHandler_Success()
        {
            var handler = new ChangePasswordHandler(_dbcontextFactory);
            var request = new ChangePasswordCommand()
            {
                Id = 1,
                NewPassword = "12345678",
                OldPassword = "1234567890"
            };
            Assert.True((await new ChangePasswordValidation().ValidateAsync(request)).IsValid);
            await handler.Handle(request, CancellationToken.None);

            using (var context = await _dbcontextFactory.CreateDbContextAsync())
            {
                var profile = await context.Profiles.Include(item => item.Authorization)
                    .FirstOrDefaultAsync(item => item.Id == 1);
                Assert.True(BCryptType.Verify(
                    "12345678", profile?.Authorization.Password, false, BCrypt.Net.HashType.SHA384));
            }
        }

        [Fact(DisplayName = "Пользователь не найден")]
        public async Task ChangePasswordHandler_UserNotFound()
        {
            var handler = new ChangePasswordHandler(_dbcontextFactory);
            var request = new ChangePasswordCommand()
            {
                Id = 10,
                NewPassword = "12345678",
                OldPassword = "1234567890"
            };
            Assert.True((await new ChangePasswordValidation().ValidateAsync(request)).IsValid);
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }

        [Fact(DisplayName = "Введен неправильный пароль")]
        public async Task ChangePasswordHandler_WrongPassword()
        {
            var handler = new ChangePasswordHandler(_dbcontextFactory);
            var request = new ChangePasswordCommand()
            {
                Id = 1,
                NewPassword = "12345678",
                OldPassword = "123412341234123"
            };
            Assert.True((await new ChangePasswordValidation().ValidateAsync(request)).IsValid);
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }
    }
}
