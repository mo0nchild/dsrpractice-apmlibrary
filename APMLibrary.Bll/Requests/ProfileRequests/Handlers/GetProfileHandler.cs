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

namespace APMLibrary.Bll.Requests.ProfileRequests.Handlers
{
    public partial class GetProfileHandler : IRequestHandler<GetProfileRequest, ProfileDto?>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        protected readonly IMapper _mapper = default!;

        public GetProfileHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base()
            => (this._mapper, this._dbcontextFactory) = (mapper, factory);

        public async Task<ProfileDto?> Handle(GetProfileRequest request, CancellationToken cancellationToken)
        {
            using var dbcontext = await _dbcontextFactory.CreateDbContextAsync(cancellationToken);

            var result = await dbcontext.Profiles.FirstOrDefaultAsync(item => item.Id == request.Id);
            if (result == null) return null;

            return _mapper.Map<ProfileDto>(result);
        }
    }
}
