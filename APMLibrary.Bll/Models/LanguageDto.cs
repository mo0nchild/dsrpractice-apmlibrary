using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Models
{
    public partial class LanguageDto : IMappingWith<Language>
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Language, LanguageDto>()
                .ForMember(target => target.Id, sourse => sourse.MapFrom(item => item.Id))
                .ForMember(target => target.Name, sourse => sourse.MapFrom(item => item.Name));
        }
    }
}
