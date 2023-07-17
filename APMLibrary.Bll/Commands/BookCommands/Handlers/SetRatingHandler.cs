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

namespace APMLibrary.Bll.Commands.BookCommands.Handlers
{
    public partial class SetRatingHandler : IRequestHandler<SetRatingCommand>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        protected readonly IMapper mapper = default!;

        public SetRatingHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base()
        {
            this.mapper = mapper;
            this._dbcontextFactory = factory;
        }
        public async Task Handle(SetRatingCommand request, CancellationToken cancellationToken)
        {
            using(var dbcontext = await this._dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                await dbcontext.Ratings.Where(item => item.ReaderId == request.ReaderId
                    && item.PublicationId == request.PublishId).ExecuteDeleteAsync();

                if ((await dbcontext.Profiles.FindAsync(request.ReaderId)) == null)
                {
                    throw new NotFoundException("Не найден аккаунт читателя", request.ReaderId);
                }
                if ((await dbcontext.Publications.FindAsync(request.PublishId)) == null)
                {
                    throw new NotFoundException("Не найдена публикация", request.PublishId);
                }
                await dbcontext.Ratings.AddRangeAsync(this.mapper.Map<Rating>(request));
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
