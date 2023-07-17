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
    public partial class DeleteBookmarkHandler : IRequestHandler<DeleteBookmarkCommand>
    {
        private readonly IDbContextFactory<LibraryDbContext> dbContextFactory = default!;
        public DeleteBookmarkHandler(IDbContextFactory<LibraryDbContext> dbContextFactory) : base()
        {
            this.dbContextFactory = dbContextFactory;
        }
        public async Task Handle(DeleteBookmarkCommand request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this.dbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var profile = await dbcontext.Profiles.Where(item => item.Id == request.ReaderId)
                    .Include(item => item.Bookmarks).FirstOrDefaultAsync();

                var publish = await dbcontext.Publications.FirstOrDefaultAsync(item => item.Id == request.BookId);
                if (profile != null && publish != null)
                {
                    profile.Bookmarks.Remove(publish);
                    await dbcontext.SaveChangesAsync(cancellationToken);
                }
            }
        }
    }
}
