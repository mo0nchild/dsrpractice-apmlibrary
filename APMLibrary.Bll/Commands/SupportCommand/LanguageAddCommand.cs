using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.SupportCommand
{
    public partial class LanguageAddCommand : IRequest, IMappingWith<Language>
    {
        public string Name { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<LanguageAddCommand, Language>()
                .ForMember(item => item.Name, options => options.MapFrom(item => item.Name));
        }
    }
}
