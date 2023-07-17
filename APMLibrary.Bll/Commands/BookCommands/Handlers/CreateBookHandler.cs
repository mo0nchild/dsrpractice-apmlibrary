using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Bll.Services.Interfaces;
using APMLibrary.Dal;
using APMLibrary.Dal.Entities;
using AutoMapper;
using iTextSharp.text.pdf;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.BookCommands.Handler
{
    public partial class CreateBookHandler : IRequestHandler<CreateBookCommand>
    {
        protected readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        protected readonly IMapper mapper = default!;
        private readonly ITextFileReader reader = default!;

        public CreateBookHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper, ITextFileReader reader)
            : base() => (this.dbcontextFactory, this.mapper, this.reader) = (factory, mapper, reader);

        public async Task Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var mappedRequest = this.mapper.Map<Publication>(request);
            using (var dbcontext = await this.dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                if((await dbcontext.Profiles.FirstOrDefaultAsync(item => item.Id == request.PublisherId)) == null)
                {
                    throw new CreateBookException("Профиль не найден");
                }
                var language = await dbcontext.Languages.FirstOrDefaultAsync(item => item.Name == request.Language);
                if (language == null) throw new CreateBookException("Язык перевода не найден");

                var publishType = await dbcontext.PublicationTypes.FirstOrDefaultAsync(item => item.Name == request.PublishType);
                if (publishType == null) throw new CreateBookException("Тип публикации не найден");

                mappedRequest.LanguageId = language.Id;
                mappedRequest.PublicationTypeId = publishType.Id;

                mappedRequest.Genres = new List<Genre>();
                foreach (var genreType in request.Genres)
                {
                    var genre = await dbcontext.Genres.FirstOrDefaultAsync(item => item.Name == genreType);
                    if (genre != null) mappedRequest.Genres.Add(genre);
                }
                if (mappedRequest.Genres == null || mappedRequest.Genres.Count <= 0)
                {
                    throw new CreateBookException("Не установлен список жанров и категорий");
                }
                mappedRequest.NumberPages = this.reader.GetPagesCount(request.BookBody);
                mappedRequest.Published = default!;

                await dbcontext.AddRangeAsync(mappedRequest);
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
