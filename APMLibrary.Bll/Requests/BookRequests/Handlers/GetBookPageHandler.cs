using APMLibrary.Bll.Services.Interfaces;
using APMLibrary.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.BookRequests.Handlers
{
    public partial class GetBookPageHandler : IRequestHandler<GetBookPageRequest, string?>
    {
        private readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        private readonly ITextFileReader textReader = default!;

        public GetBookPageHandler(IDbContextFactory<LibraryDbContext> factory, ITextFileReader reader) : base()
        {
            this.dbcontextFactory = factory;
            this.textReader = reader;
        }
        public async Task<string?> Handle(GetBookPageRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this.dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                var publish = await dbcontext.Publications.FirstOrDefaultAsync(item => item.Id == request.BookId);
                if (publish != null) return await this.textReader.ReadPageAsync(publish.Body, request.Page);
            }
            return default;
        }
    }
}
