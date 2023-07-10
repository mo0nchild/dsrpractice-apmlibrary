using APMLibrary.Bll.Commands.ProfileCommands;
using APMLibrary.Bll.Common.Exceptions;
using APMLibrary.Dal;
using APMLibrary.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.ProfileCommands.Handlers
{
    using IMapper = AutoMapper.IMapper;
    public partial class EditProfileHandler : IRequestHandler<EditProfileCommand>
    {
        protected readonly IDbContextFactory<LibraryDbContext> _dbcontextFactory = default!;
        protected readonly IMapper _mapper = default!;

        public EditProfileHandler(IDbContextFactory<LibraryDbContext> factory, IMapper mapper) : base()
            => (_mapper, _dbcontextFactory) = (mapper, factory);

        public async Task Handle(EditProfileCommand request, CancellationToken cancellationToken)
        {
            var mappedModel = _mapper.Map<Profile>(request);
            using (var dbcontext = await _dbcontextFactory.CreateDbContextAsync(cancellationToken))
            {
                var requestResult = await dbcontext.Profiles.FirstOrDefaultAsync(item => item.Id == request.Id);
                if (requestResult == null)
                {
                    throw new NotFoundException("Невозможно изменить данные профиля", request.Id);
                }
                requestResult.Surname = mappedModel.Surname;
                requestResult.Name = mappedModel.Name;

                requestResult.Email = mappedModel.Email;
                requestResult.Phone = mappedModel.Phone;

                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
