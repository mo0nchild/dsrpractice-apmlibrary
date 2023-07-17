using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Dal;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.BookCommands.Handler
{
    public partial class DeleteBookHandler : IRequestHandler<DeleteBookCommand>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        public DeleteBookHandler(IDbContextFactory<LibraryDbContext> factory) : base()
            => this._dbcontextFactory = factory;

        public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                var result = await dbcontext.Publications.FirstOrDefaultAsync(item => item.Id == request.Id);
                if (result == null) throw new NotFoundException("Публикация не найдена", request.Id);

                dbcontext.Publications.RemoveRange(result);
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
