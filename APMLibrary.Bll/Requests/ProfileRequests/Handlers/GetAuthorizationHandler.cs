using APMLibrary.Bll.Models.ProfileModels;
using APMLibrary.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.ProfileRequests.Handlers
{
    using IMapper = AutoMapper.IMapper;
    using BCryptType = BCrypt.Net.BCrypt;
    public partial class GetAuthorizationHandler : IRequestHandler<GetAuthorizationRequest, AuthorizationDto?>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        protected readonly IMapper _mapper = default!;

        public GetAuthorizationHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base()
            => (_mapper, _dbcontextFactory) = (mapper, factory);

        public async Task<AuthorizationDto?> Handle(GetAuthorizationRequest request, CancellationToken cancellationToken)
        {
            using var dbcontext = await _dbcontextFactory.CreateDbContextAsync(cancellationToken);
            var verifyPassword = (string hashPassword) =>
            {
                try { return BCryptType.Verify(request.Password, hashPassword, false, BCrypt.Net.HashType.SHA384); }
                catch (BCrypt.Net.SaltParseException) { return false; }
            };
            var profiles = await dbcontext.Authorizations.Where(item => item.Login == request.Login).ToListAsync();
            var result = profiles.FirstOrDefault(item => item.Login == request.Login && verifyPassword(item.Password));

            if (result == null) return null;
            return _mapper.Map<AuthorizationDto>(result);
        }
    }
}
