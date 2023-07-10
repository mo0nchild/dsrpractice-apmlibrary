using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Dal;
using APMLibrary.Dal.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.SupportCommand.Handlers
{
    public partial class BookTypeAddHandler : IRequestHandler<BookTypeAddCommand>
    {
        private readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        private readonly IMapper _mapper = default!;

        public BookTypeAddHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base() 
            => (this._dbcontextFactory, this._mapper) = (factory, mapper);

        public async Task Handle(BookTypeAddCommand request, CancellationToken cancellationToken)
        {
            var mapperModel = this._mapper.Map<PublicationType>(request);
            using (var dbcontext = await this._dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                if((await dbcontext.PublicationTypes.FirstOrDefaultAsync(item => item.Name == request.Name)) != null)
                {
                    throw new CollisionException("Тип публикации уже хранится", typeof(BookTypeAddCommand));
                }
                await dbcontext.PublicationTypes.AddRangeAsync(mapperModel);
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
