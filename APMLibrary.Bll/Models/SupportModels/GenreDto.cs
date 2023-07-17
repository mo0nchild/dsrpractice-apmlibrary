using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Models.SupportModels
{
    public partial class GenreDto : IMappingWith<Genre>
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public CategoryDto Category { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Genre, GenreDto>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Name, options => options.MapFrom(item => item.Name))
                .ForMember(item => item.Category, options => options.MapFrom(item => item.Category));
        }
    }
}
