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

namespace APMLibrary.Bll.Commands.BookCommands.Handler
{
    public partial class UpdateBookHandler : IRequestHandler<UpdateBookCommand>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        protected readonly IMapper _mapper = default!;

        public UpdateBookHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base()
            => (this._dbcontextFactory, this._mapper) = (factory, mapper);

        public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                var publish = await dbcontext.Publications.FirstOrDefaultAsync(item => item.Id == request.BookId);
                if (publish == null)
                {
                    throw new NotFoundException("Запись о публикации не найдена", request.BookId);
                }
                publish.Title = request.Title;
                publish.AuthorName = request.AuthorName;
                publish.YearIssue = request.YearIssue;
                publish.Description = request.Description;

                var language = await dbcontext.Languages.FirstOrDefaultAsync(item => item.Name == request.Language);
                if (language == null) throw new CreateBookException("Язык перевода не найден");

                var publishType = await dbcontext.PublicationTypes.FirstOrDefaultAsync(item => item.Name == request.PublishType);
                if (publishType == null) throw new CreateBookException("Тип публикации не найден");

                publish.LanguageId = language.Id;
                publish.PublicationTypeId = publishType.Id;

                publish.Genres.Clear();
                foreach (var genreType in request.Genres)
                {
                    var genre = await dbcontext.Genres.FirstOrDefaultAsync(item => item.Name == genreType);
                    if (genre != null) publish.Genres.Add(genre);
                }
                if (publish.Genres == null || publish.Genres.Count <= 0)
                {
                    throw new CreateBookException("Не установлен список жанров и категорий");
                }
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
