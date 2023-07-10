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
    public partial class ChangeImageHandler : IRequestHandler<ChangeImageCommand>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        public ChangeImageHandler(IDbContextFactory<LibraryDbContext> factory) : base()
            => _dbcontextFactory = factory;

        public async Task Handle(ChangeImageCommand request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await _dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                var requestResult = await dbcontext.Profiles.Where(item => item.Id == request.Id)
                    .ExecuteUpdateAsync(item => item.SetProperty(prop => prop.Image, prop => request.Image));

                if (requestResult <= 0) throw new NotFoundException("Неудалось изменить изображение", request.Id);
            }
        }
    }
}
