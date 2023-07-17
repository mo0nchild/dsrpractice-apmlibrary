using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Models.SupportModels;

namespace APMLibrary.Web.ViewModels.SupportViewModels
{
    public partial class BookTypeViewModel : IMappingWith<BookTypeDto>
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<BookTypeDto, BookTypeViewModel>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Name, options => options.MapFrom(item => item.Name));
        }
    }
}
