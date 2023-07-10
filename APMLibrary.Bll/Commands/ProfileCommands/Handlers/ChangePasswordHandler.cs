using APMLibrary.Bll.Commands.ProfileCommands;
using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.ProfileCommands.Handlers
{
    using BCryptType = BCrypt.Net.BCrypt;
    public partial class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        public ChangePasswordHandler(IDbContextFactory<LibraryDbContext> factory) : base() => _dbcontextFactory = factory;

        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var verifyPassword = (string hashPassword) =>
            {
                try { return BCryptType.Verify(request.OldPassword, hashPassword, false, BCrypt.Net.HashType.SHA384); }
                catch (BCrypt.Net.SaltParseException) { return false; }
            };
            using (var dbcontext = await _dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                var profile = await dbcontext.Authorizations.FirstOrDefaultAsync(item => item.ProfileId == request.Id);
                if (profile == null || !verifyPassword(profile.Password))
                {
                    throw new NotFoundException("Профиль не найден или введён неверный пароль", request.Id);
                }
                profile.Password = BCryptType.HashPassword(request.NewPassword);
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
