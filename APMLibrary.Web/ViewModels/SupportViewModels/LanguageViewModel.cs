using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Models.SupportModels;

namespace APMLibrary.Web.ViewModels.SupportViewModels
{
    public partial class LanguageViewModel : IMappingWith<LanguageDto>
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<LanguageDto, LanguageViewModel>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Name, options => options.MapFrom(item => item.Name));
        }
    }
}
