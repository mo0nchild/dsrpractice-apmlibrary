using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands
{
    public partial class CreateProfileCommand : IRequest<int>, IMappingWith<Profile>
    {
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Surname { get; set; } = default!;

        public string Email { get; set; } = default!;
        public string? Phone { get; set; } = default!;
        public Guid Reference { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<CreateProfileCommand, Profile>()
                .ForMember(target => target.Name, sourse => sourse.MapFrom(item => item.UserName))
                .ForMember(target => target.Surname, sourse => sourse.MapFrom(item => item.Surname))
                .ForMember(target => target.Reference, sourse => sourse.MapFrom(item => Guid.NewGuid()))

                .ForMember(target => target.Email, sourse => sourse.MapFrom(item => item.Email))
                .ForMember(target => target.Phone, sourse => sourse.MapFrom(item => item.Phone));
        }
    }
}
