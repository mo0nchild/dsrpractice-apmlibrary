using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Models;

namespace APMLibrary.Web.ViewModels.SupportViewModels
{
    public partial class GenreViewModel : IMappingWith<GenreDto>
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;

        public CategoryViewModel Category { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<GenreDto, GenreViewModel>()
                .ForMember(item => item.Category, options => options.MapFrom(item => item.Category))

                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Name, options => options.MapFrom(item => item.Name));
        }
    }
}
