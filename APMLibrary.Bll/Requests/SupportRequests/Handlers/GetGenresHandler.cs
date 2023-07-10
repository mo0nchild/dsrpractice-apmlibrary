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

namespace APMLibrary.Bll.Requests.SupportRequests.Handlers
{
    public partial class GetGenresHandler : IRequestHandler<GetGenresRequest, IEnumerable<GenreDto>>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        protected readonly IMapper _mapper = default!;

        public GetGenresHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base()
            => (_mapper, _dbcontextFactory) = (mapper, factory);

        public async Task<IEnumerable<GenreDto>> Handle(GetGenresRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                var requestResult = await dbcontext.Genres.Include(item => item.Category).ToListAsync();
                return this._mapper.Map<IEnumerable<GenreDto>>(requestResult);
            }
        }
    }
}
