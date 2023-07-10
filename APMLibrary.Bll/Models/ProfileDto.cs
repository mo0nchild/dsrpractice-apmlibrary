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
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Phone { get; set; } = default!;

        public Guid Reference { get; set; } = Guid.Empty;
        public byte[]? Image { get; set; } = null;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<byte[]?, byte[]?>().ConvertUsing((value, destination) => value == null ? null : value);

            profile.CreateMap<Profile, ProfileDto>()
                .ForMember(target => target.Id, sourse => sourse.MapFrom(item => item.Id))
                .ForMember(target => target.Name, sourse => sourse.MapFrom(item => item.Name))
                .ForMember(target => target.Surname, sourse => sourse.MapFrom(item => item.Surname))
                .ForMember(target => target.Reference, sourse => sourse.MapFrom(item => item.Reference))
                .ForMember(target => target.Image, sourse => sourse.MapFrom(item => item.Image))

                .ForMember(target => target.Email, sourse => sourse.MapFrom(item => item.Email))
                .ForMember(target => target.Phone, sourse => sourse.MapFrom(item => item.Phone));
        }
    }
}
