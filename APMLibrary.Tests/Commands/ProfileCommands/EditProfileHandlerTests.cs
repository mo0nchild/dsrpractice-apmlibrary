using APMLibrary.Bll.Commands.ProfileCommands.Handlers;
using APMLibrary.Bll.Commands.ProfileCommands.Validations;
using APMLibrary.Bll.Commands.ProfileCommands;
using APMLibrary.Dal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APMLibrary.Tests.Common;
using APMLibrary.Bll.Common.Exceptions;

namespace APMLibrary.Tests.Commands.ProfileCommands
{
    [Collection("RequestsCollection")]
    public sealed class EditProfileHandlerTests : object
    {
        private readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        private readonly IMapper _mapper = default!;

        public EditProfileHandlerTests(RequestsTestFixture fixture) : base()
        {
            _dbcontextFactory = fixture.Factory;
            _mapper = fixture.Mapper;
        }

        [Fact(DisplayName = "Обновление профиля пользователя")]
        public async Task EditProfileHandler_Success()
        {
            var handler = new EditProfileHandler(_dbcontextFactory, _mapper);
            var request = new EditProfileCommand()
            {
                Id = 1,
                Email = "usertest@gmail.com",
                Phone = "+79009009090",
                Surname = "Иванa",
                Name = "Иванович",
            };
            await handler.Handle(request, CancellationToken.None);
            using (var context = await _dbcontextFactory.CreateDbContextAsync())
            {
                var profile = await context.Profiles.FirstOrDefaultAsync(item => item.Id == 1);
                Assert.NotNull(profile);

                Assert.Equal("Иванович", profile.Name);
                Assert.Equal("Иванa", profile.Surname);
                Assert.Equal("usertest@gmail.com", profile.Email);
            }
        }

        [Fact(DisplayName = "Профиль пользователя не найден")]
        public async Task EditProfileHandler_NotFound()
        {
            var handler = new EditProfileHandler(_dbcontextFactory, _mapper);
            var request = new EditProfileCommand()
            {
                Id = 10,
                Email = "usertest@gmail.com",
                Phone = "+79009009090",
                Surname = "Иванa",
                Name = "Иванович",
            };
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
        }
    }
}
