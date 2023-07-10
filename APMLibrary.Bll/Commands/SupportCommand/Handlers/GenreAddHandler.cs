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
    public partial class GenreAddHandler : IRequestHandler<GenreAddCommand>
    {
        private readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        private readonly IMapper _mapper = default!;

        public GenreAddHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base()
            => (this._dbcontextFactory, this._mapper) = (factory, mapper);

        public async Task Handle(GenreAddCommand request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                if ((await dbcontext.Genres.FirstOrDefaultAsync(item => item.Name == request.Name)) != null)
                {
                    throw new CollisionException("Жанр изданий уже хранится", typeof(GenreAddCommand));
                }
                var category = await dbcontext.Categories.FirstOrDefaultAsync(item => item.Name == request.Category);
                if (category == null) throw new NotFoundException("Не удалось найти категорию");

                await dbcontext.Genres.AddRangeAsync(new Genre()
                {
                    Name = request.Name,
                    CategoryId = category.Id,
                });
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
