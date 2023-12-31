﻿using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Models.ProfileModels;
using AutoMapper;

namespace APMLibrary.Web.ViewModels.ProfileViewModels
{
    public partial class ProfileNavViewModel : object, IMappingWith<ProfileDto>
    {
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;

        public byte[]? Image { get; set; } = null;

        public virtual void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<ProfileDto, ProfileNavViewModel>()
                .ForMember(target => target.Name, sourse => sourse.MapFrom(item => item.Name))
                .ForMember(target => target.Surname, sourse => sourse.MapFrom(item => item.Surname))
                .ForMember(target => target.Image, sourse => sourse.MapFrom(item => item.Image));
        }
    }
}
