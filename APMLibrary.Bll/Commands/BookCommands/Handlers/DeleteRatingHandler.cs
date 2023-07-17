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
    public partial class DeleteRatingHandler : IRequestHandler<DeleteRatingCommand>
    {
        private readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        public DeleteRatingHandler(IDbContextFactory<LibraryDbContext> dbcontextFactory) : base()
        {
            this.dbcontextFactory = dbcontextFactory;
        }
        public async Task Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this.dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                var rating = await dbcontext.Ratings.FirstOrDefaultAsync(item => item.Id == request.RatingId);
                if (rating == null) throw new NotFoundException("Отзыв не найден", request.RatingId);

                dbcontext.Ratings.RemoveRange(rating);
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
