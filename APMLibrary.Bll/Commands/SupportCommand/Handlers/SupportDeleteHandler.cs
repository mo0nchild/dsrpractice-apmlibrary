using APMLibrary.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.SupportCommand.Handlers
{
    public partial class SupportDeleteHandler : IRequestHandler<SupportDeleteCommand>
    {
        private readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        public SupportDeleteHandler(IDbContextFactory<LibraryDbContext> factory) : base() 
            => this._dbcontextFactory = factory;

        public async Task Handle(SupportDeleteCommand request, CancellationToken cancellationToken)
        {
            using (var dbcontext =  await this._dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                switch (request.SupportType)
                {
                    case SupportType.Category:
                        await dbcontext.Categories.Where(item => item.Id == request.SupportId).ExecuteDeleteAsync();
                        break;
                    case SupportType.BookType:
                        await dbcontext.PublicationTypes.Where(item => item.Id == request.SupportId).ExecuteDeleteAsync();
                        break;
                    case SupportType.Language:
                        await dbcontext.Languages.Where(item => item.Id == request.SupportId).ExecuteDeleteAsync();
                        break;
                    case SupportType.Genre:
                        await dbcontext.Genres.Where(item => item.Id == request.SupportId).ExecuteDeleteAsync();
                        break;
                }
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
