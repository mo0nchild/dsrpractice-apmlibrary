using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Models.ProfileModels;
using APMLibrary.Dal.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.ProfileCommands
{
    public partial class ChangeRoleCommand : IRequest, IMappingWith<Authorization>
    {
        public int Id { get; set; } = default!;
        public ProfileType ProfileType { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<ProfileType, AccountType>().ConvertUsing((value, dest) => value switch
            {
                ProfileType.Admin => AccountType.Admin,
                ProfileType.User => AccountType.User,
                _ => throw new InvalidOperationException("Неверное значение роли профиля")
            });
        }
    }
}
