using APMLibrary.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.Handlers
{
    using IMapper = AutoMapper.IMapper;
    public partial class GetAuthorizationHandler : IRequestHandler<GetAuthorizationRequest, int?>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        protected readonly IMapper _mapper = default!;

        public GetAuthorizationHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base()
            => (this._mapper, this._dbcontextFactory) = (mapper, factory);

        public async Task<int?> Handle(GetAuthorizationRequest request, CancellationToken cancellationToken)
        {
            using var dbcontext = await this._dbcontextFactory.CreateDbContextAsync(cancellationToken);

            return (await dbcontext.Authorizations.FirstOrDefaultAsync(
                item => item.Login == request.Login && item.Password == request.Password))?.ProfileId;
        }
    }
}
