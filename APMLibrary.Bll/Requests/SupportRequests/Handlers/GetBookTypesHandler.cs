using APMLibrary.Bll.Models.SupportModels;
using APMLibrary.Dal;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.SupportRequests.Handlers
{
    public partial class GetBookTypesHandler : IRequestHandler<GetBookTypesRequest, IEnumerable<BookTypeDto>>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        protected readonly IMapper _mapper = default!;

        public GetBookTypesHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base()
            => (_mapper, _dbcontextFactory) = (mapper, factory);

        public async Task<IEnumerable<BookTypeDto>> Handle(GetBookTypesRequest request, CancellationToken cancellationToken)
        {
            using var dbcontext = await this._dbcontextFactory.CreateDbContextAsync(cancellationToken);
            return this._mapper.Map<IEnumerable<BookTypeDto>>(await dbcontext.PublicationTypes.ToListAsync());
        }
    }
}
