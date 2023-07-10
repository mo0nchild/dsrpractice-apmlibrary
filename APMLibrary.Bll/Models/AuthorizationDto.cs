using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Models
{
    public enum ProfileType : sbyte { User, Admin }

    public partial class AuthorizationDto : object, IMappingWith<Authorization>
    {
        public int Id { get; set; } = default!;
        public ProfileType ProfileType { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<AccountType, ProfileType>().ConvertUsing(new ProfileEnumConverter());

            profile.CreateMap<Authorization, AuthorizationDto>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.ProfileId))
                .ForMember(item => item.ProfileType, options => options.MapFrom(item => item.AccountType));
        }
    }
}
