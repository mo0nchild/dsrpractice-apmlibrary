using APMLibrary.Bll.Common.Exceptions;
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
    public partial class SetBookmarkHandler : IRequestHandler<SetBookmarkCommand>
    {
        private readonly IDbContextFactory<LibraryDbContext> dbContextFactory = default!;
        public SetBookmarkHandler(IDbContextFactory<LibraryDbContext> factory) : base()
        {
            this.dbContextFactory = factory;
        }
        public async Task Handle(SetBookmarkCommand request, CancellationToken cancellationToken)
        {
            using(var dbcontext = await this.dbContextFactory.CreateDbContextAsync(cancellationToken))
            {
                var reader = await dbcontext.Profiles.Include(item => item.Bookmarks)
                    .FirstOrDefaultAsync(item => item.Id == request.ReaderId);

                if (reader == null) throw new NotFoundException("Пользователь не найден", request.ReaderId);
                if (reader.Bookmarks.FirstOrDefault(item => item.Id == request.PublishId) != null)
                {
                    throw new CollisionException("Публикация уже добавлена в закладки", typeof(SetBookmarkCommand));
                }
                var publish = await dbcontext.Publications.FirstOrDefaultAsync(item => item.Id == request.PublishId);
                if (publish == null)
                {
                    throw new NotFoundException("Публикация не найдена", request.PublishId);
                }
                reader.Bookmarks.Add(publish);
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
