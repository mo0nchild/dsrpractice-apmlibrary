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
    public partial class LanguageAddHandler : IRequestHandler<LanguageAddCommand>
    {
        private readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        private readonly IMapper _mapper = default!;

        public LanguageAddHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base()
            => (this._dbcontextFactory, this._mapper) = (factory, mapper);

        public async Task Handle(LanguageAddCommand request, CancellationToken cancellationToken)
        {
            var mappedModel = this._mapper.Map<Language>(request);
            using (var dbcontext = await this._dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                if ((await dbcontext.Languages.FirstOrDefaultAsync(item => item.Name == request.Name)) != null)
                {
                    throw new CollisionException("Язык перевода уже хранится", typeof(LanguageAddCommand));
                }
                await dbcontext.Languages.AddRangeAsync(mappedModel);
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
