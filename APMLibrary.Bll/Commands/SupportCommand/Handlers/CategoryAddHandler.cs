using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Dal;
using APMLibrary.Dal.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.SupportCommand.Handlers
{
    public partial class CategoryAddHandler : IRequestHandler<CategoryAddCommand>
    {
        private readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        private readonly IMapper _mapper = default!;

        public CategoryAddHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base()
            => (this._dbcontextFactory, this._mapper) = (factory, mapper);

        public async Task Handle(CategoryAddCommand request, CancellationToken cancellationToken)
        {
            var mapperModel = this._mapper.Map<Category>(request);
            using (var dbcontext = await this._dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                if ((await dbcontext.Categories.FirstOrDefaultAsync(item => item.Name == request.Name)) != null)
                {
                    throw new CollisionException("Категория издания уже хранится", typeof(CategoryAddCommand));
                }
                await dbcontext.Categories.AddRangeAsync(mapperModel);
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
