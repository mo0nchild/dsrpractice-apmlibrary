using APMLibrary.Bll.Models;
using APMLibrary.Dal;
using AutoMapper;
using iTextSharp.text.pdf.parser;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.BookRequests.Handlers
{
    public partial class GetBooksListHandler : IRequestHandler<GetBooksListRequest, BooksListDto>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        protected readonly IMapper _mapper = default!;

        public GetBooksListHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base()
            => (this._dbcontextFactory, this._mapper) = (factory, mapper);

        public async Task<BooksListDto> Handle(GetBooksListRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                var requestResult = dbcontext.Publications.Include(item => item.Language).Include(item => item.Genres)
                    .Include(item => item.BookCover).Include(item => item.Ratings)
                    .Where(item =>
                        (request.TextFilter == null ? true : Regex.IsMatch(item.Title, request.TextFilter)) &&
                        (request.GenreFilter == null ? true : item.Genres.FirstOrDefault(op => op.Name == request.GenreFilter) != null) &&
                        //(request.IsPublished == null ? true : item.Published == request.IsPublished.Value) &&
                        (request.LanguageFilter == null ? true : item.Language.Name == request.LanguageFilter));

                var requestObjects = await requestResult.Include(item => item.Ratings)
                    .Skip(request.Skip).Take(request.Take).ToListAsync();

                requestObjects = request.SortingType switch
                {
                    SortingType.ByDate => requestObjects.OrderByDescending(item => item.YearIssue).ToList(),
                    SortingType.ByRating => requestObjects.OrderByDescending(item
                        => item.Ratings.Sum(op => (double)op.Value / item.Ratings.Count())).ToList(),

                    SortingType.ByName => requestObjects.OrderBy(item => item.Title).ToList(),
                    SortingType.ByPageCount => requestObjects.OrderBy(item => item.NumberPages).ToList(),

                    _ => throw new InvalidOperationException()
                };
                return new BooksListDto()
                {
                    AllBooksCount = await requestResult.CountAsync(),
                    Books = this._mapper.Map<List<BookDto>>(requestObjects),
                };
            }
        }
    }
}
