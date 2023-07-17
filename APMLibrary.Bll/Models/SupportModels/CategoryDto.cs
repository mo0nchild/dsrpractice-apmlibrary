using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Models.SupportModels
{
    public partial class CategoryDto : IMappingWith<Category>
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Category, CategoryDto>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Name, options => options.MapFrom(item => item.Name));
        }
    }
}
