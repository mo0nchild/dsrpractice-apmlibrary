using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.ProfileCommands
{
    public partial class EditProfileCommand : IRequest, IMappingWith<Profile>
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;

        public string Email { get; set; } = default!;
        public string? Phone { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<EditProfileCommand, Profile>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Name, options => options.MapFrom(item => item.Name))
                .ForMember(item => item.Surname, options => options.MapFrom(item => item.Surname))

                .ForMember(item => item.Email, options => options.MapFrom(item => item.Email))
                .ForMember(item => item.Phone, options => options.MapFrom(item => item.Phone));
        }
    }
}
