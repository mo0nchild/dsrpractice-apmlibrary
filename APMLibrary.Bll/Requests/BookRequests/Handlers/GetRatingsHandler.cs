using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Bll.Models;
using APMLibrary.Dal;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.BookRequests.Handlers
{
    public partial class GetRatingsHandler : IRequestHandler<GetRatingsRequest, RatingsListDto>
    {
        protected readonly IDbContextFactory<LibraryDbContext> contextFactory = default!;
        protected readonly IMapper mapper = default!;

        public GetRatingsHandler(IDbContextFactory<LibraryDbContext> contextFactory, IMapper mapper) : base()
        {
            this.mapper = mapper;
            this.contextFactory = contextFactory;
        }
        public async Task<RatingsListDto> Handle(GetRatingsRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this.contextFactory.CreateDbContextAsync(cancellationToken))
            {
                if ((await dbcontext.Publications.FindAsync(request.BookId)) == null)
                {
                    throw new NotFoundException("Публикация не найдена", request.BookId);
                }
                var databaseRequest = dbcontext.Ratings.Where(item => item.PublicationId == request.BookId)
                    .Include(item => item.Reader);

                var ratingsList = await databaseRequest.Skip(request.Skip).Take(request.Take).ToListAsync();
                return new RatingsListDto()
                {
                    AllCount = await databaseRequest.CountAsync(),
                    Ratings = this.mapper.Map<IEnumerable<RatingDto>>(ratingsList),
                };
            }
        }
    }
}
