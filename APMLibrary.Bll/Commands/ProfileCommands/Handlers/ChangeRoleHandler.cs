using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Dal;
using APMLibrary.Dal.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.ProfileCommands.Handlers
{
    public partial class ChangeRoleHandler : IRequestHandler<ChangeRoleCommand>
    {
        protected readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        protected readonly IMapper mapper = default!;
        public ChangeRoleHandler(IDbContextFactory<LibraryDbContext> dbcontextFactory, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbcontextFactory = dbcontextFactory;
        }
        public async Task Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this.dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                var profile = await dbcontext.Profiles.Where(item => item.Id == request.Id)
                    .Include(item => item.Authorization).FirstOrDefaultAsync();

                if (profile == null) throw new NotFoundException("Пользователь не найден", request.Id);
                profile.Authorization.AccountType = this.mapper.Map<AccountType>(request.ProfileType);

                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
