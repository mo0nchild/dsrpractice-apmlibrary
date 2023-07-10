using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.SupportCommand
{
    public partial class BookTypeAddCommand : IRequest, IMappingWith<PublicationType>
    {
        public string Name { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<BookTypeAddCommand, PublicationType>()
                .ForMember(item => item.Name, options => options.MapFrom(item => item.Name));
        }
    }
}
