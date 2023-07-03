using APMLibrary.Bll.Commands;
using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Models
{
    public partial class ProfileDto : object, IMappingWith<Profile>
    {
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Phone { get; set; } = default!;

        public Guid Reference { get; set; } = Guid.Empty;
        public byte[]? Image { get; set; } = default!;

        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Profile, ProfileDto>()
                .ForMember(target => target.Name, sourse => sourse.MapFrom(item => item.Name))
                .ForMember(target => target.Surname, sourse => sourse.MapFrom(item => item.Surname))
                .ForMember(target => target.Reference, sourse => sourse.MapFrom(item => item.Reference))

                .ForMember(target => target.Email, sourse => sourse.MapFrom(item => item.Email))
                .ForMember(target => target.Phone, sourse => sourse.MapFrom(item => item.Phone))

                .ForMember(target => target.Login, sourse => sourse.MapFrom(item => item.Authorization.Login))
                .ForMember(target => target.Password, sourse => sourse.MapFrom(item => item.Authorization.Password));
        }
    }
}
