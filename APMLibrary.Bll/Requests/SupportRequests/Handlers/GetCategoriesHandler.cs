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
    public partial class GetCategoriesHandler : IRequestHandler<GetCategoriesRequest, IEnumerable<CategoryDto>>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        protected readonly IMapper _mapper = default!;

        public GetCategoriesHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base()
            => (_mapper, _dbcontextFactory) = (mapper, factory);

        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
        {
            using var dbcontext = await this._dbcontextFactory.CreateDbContextAsync(cancellationToken);
            return this._mapper.Map<IEnumerable<CategoryDto>>(await dbcontext.Categories.ToListAsync());
        }
    }
}
