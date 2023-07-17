using APMLibrary.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.BookCommands.Handlers
{
    public partial class SetPublishedHandler : IRequestHandler<SetPublishedCommand>
    {
        private readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        public SetPublishedHandler(IDbContextFactory<LibraryDbContext> dbcontextFactory) : base()
        {
            this.dbcontextFactory = dbcontextFactory;
        }
        public async Task Handle(SetPublishedCommand request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this.dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                await dbcontext.Publications.Where(item => item.Id == request.BookId)
                    .ExecuteUpdateAsync(item => item.SetProperty(p => p.Published, request.IsPublished));
            }
        }
    }
}
