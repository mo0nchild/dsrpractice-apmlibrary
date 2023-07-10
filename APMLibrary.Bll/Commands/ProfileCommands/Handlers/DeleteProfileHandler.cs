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
    public partial class DeleteProfileHandler : IRequestHandler<DeleteProfileCommand>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        public DeleteProfileHandler(IDbContextFactory<LibraryDbContext> factory) : base()
            => _dbcontextFactory = factory;

        public async Task Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await _dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                var result = await dbcontext.Profiles.Where(item => item.Id == request.Id).ExecuteDeleteAsync();
                if (result <= 0) throw new NotFoundException("Невозможно удалить профиль", request.Id);
            }
        }
    }
}
