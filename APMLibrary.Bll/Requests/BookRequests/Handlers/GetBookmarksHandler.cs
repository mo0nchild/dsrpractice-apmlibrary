using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Bll.Models.BookModels;
using APMLibrary.Bll.Models.ProfileModels;
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
    public partial class GetBookmarksHandler : IRequestHandler<GetBookmarksRequest, IEnumerable<BookmarkDto>>
    {
        private readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        private readonly IMapper mapper = default!;

        public GetBookmarksHandler(IDbContextFactory<LibraryDbContext> dbcontextFactory, IMapper mapper) : base()
        {
            this.mapper = mapper;
            this.dbcontextFactory = dbcontextFactory;
        }
        public async Task<IEnumerable<BookmarkDto>> Handle(GetBookmarksRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this.dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                var reader = await dbcontext.Profiles.Where(item => item.Id == request.ReaderId)
                    .Include(item => item.Bookmarks).ThenInclude(item => item.Ratings)
                    .FirstOrDefaultAsync();

                if (reader == null) throw new NotFoundException("Профиль читателя не найден", request.ReaderId);
                return this.mapper.Map<IEnumerable<BookmarkDto>>(reader.Bookmarks);
            }
        }
    }
}
