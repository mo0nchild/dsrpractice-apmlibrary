using APMLibrary.Bll.Commands.ProfileCommands;
using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Dal;
using APMLibrary.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.ProfileCommands.Handlers
{
    using IMapper = AutoMapper.IMapper;
    using BCrypt = BCrypt.Net.BCrypt;
    public partial class CreateProfileHandler : IRequestHandler<CreateProfileCommand, int>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        protected readonly IMapper _mapper = default!;

        public CreateProfileHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base()
            => (_mapper, _dbcontextFactory) = (mapper, factory);

        public async Task<int> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            var authorizationModel = new Authorization()
            {
                Profile = _mapper.Map<Profile>(request),
                Login = request.Login,
                Password = BCrypt.HashPassword(request.Password),
                AccountType = AccountType.User,
            };
            using (var dbcontext = await _dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                var loginCollision = await dbcontext.Authorizations.FirstOrDefaultAsync(item => item.Login == request.Login);
                if (loginCollision != null) throw new CreateProfileException("Логин уже используется другим");

                await dbcontext.Authorizations.AddRangeAsync(authorizationModel);
                await dbcontext.SaveChangesAsync();
            }
            return authorizationModel.Profile.Id;
        }
    }
}
