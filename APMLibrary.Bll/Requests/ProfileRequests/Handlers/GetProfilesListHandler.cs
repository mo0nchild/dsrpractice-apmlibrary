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

namespace APMLibrary.Bll.Requests.ProfileRequests.Handlers
{
    public partial class GetProfilesListHandler : IRequestHandler<GetProfilesListRequest, ProfilesListDto>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        protected readonly IMapper _mapper = default!;

        public GetProfilesListHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base()
            => (this._mapper, this._dbcontextFactory) = (mapper, factory);

        public async Task<ProfilesListDto> Handle(GetProfilesListRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                var requestResult = await dbcontext.Profiles.Skip(request.Skip).Take(request.Take).ToListAsync();
                return new ProfilesListDto()
                {
                    AllCount = await dbcontext.Profiles.CountAsync(),
                    Profiles = this._mapper.Map<IEnumerable<ProfileDto>>(requestResult),
                };
            }
        }
    }
}
