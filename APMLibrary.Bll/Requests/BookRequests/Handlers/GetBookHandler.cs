using APMLibrary.Bll.Models.BookModels;
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
    public partial class GetBookHandler : IRequestHandler<GetBookRequest, BookDto?>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        protected readonly IMapper _mapper = default!;

        public GetBookHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base()
            => (this._dbcontextFactory, this._mapper) = (factory, mapper);

        public async Task<BookDto?> Handle(GetBookRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                var requestResult = await dbcontext.Publications.Where(item => item.Id == request.Id)
                    .Include(item => item.Publisher)
                    .Include(item => item.Genres)
                    .Include(item => item.PublicationType)
                    .Include(item => item.Ratings)
                    .Include(item => item.Language).FirstOrDefaultAsync();

                return this._mapper.Map<BookDto>(requestResult);
            }
        }
    }
}
