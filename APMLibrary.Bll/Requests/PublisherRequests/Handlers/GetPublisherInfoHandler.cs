using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Bll.Models.BookModels;
using APMLibrary.Bll.Models.PublisherModels;
using APMLibrary.Dal;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.PublisherRequests.Handlers
{
    public partial class GetPublisherInfoHandler : IRequestHandler<GetPublisherInfoRequest, IEnumerable<PublicationDto>>
    {
        private readonly IDbContextFactory<LibraryDbContext> dbcontextFactory = default!;
        private readonly IMapper mapper = default!;

        public GetPublisherInfoHandler(IDbContextFactory<LibraryDbContext> dbcontextFactory, IMapper mapper) : base()
        {
            this.mapper = mapper;
            this.dbcontextFactory = dbcontextFactory;
        }
        public async Task<IEnumerable<PublicationDto>> Handle(GetPublisherInfoRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this.dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                var publisher = await dbcontext.Profiles.Where(item => item.Id == request.PublisherId)
                    .Include(item => item.Publications).ThenInclude(item => item.Ratings)
                    .Include(item => item.Publications).ThenInclude(item => item.PublicationType)
                    .Include(item => item.Publications).ThenInclude(item => item.Language)
                    .FirstOrDefaultAsync();

                if (publisher == null) throw new NotFoundException("Издатель не найден", request.PublisherId);
                return this.mapper.Map<IEnumerable<PublicationDto>>(publisher.Publications);
            }
        }
    }
}
