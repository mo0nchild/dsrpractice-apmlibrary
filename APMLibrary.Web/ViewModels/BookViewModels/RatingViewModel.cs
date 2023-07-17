using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Models.BookModels;
using APMLibrary.Web.ViewModels.ProfileViewModels;

namespace APMLibrary.Web.ViewModels.BookViewModels
{
    public partial class RatingViewModel : IMappingWith<RatingDto>
    {
        public int Id { get; set; } = default!;
        public int Rating { get; set; } = default!;
        public DateTime DateTime { get; set; } = default!;

        public string? Comment { get; set; } = default;
        public ProfileNavViewModel Reader { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<RatingDto, RatingViewModel>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Rating, options => options.MapFrom(item => item.Value))
                .ForMember(item => item.DateTime, options => options.MapFrom(item => item.DateTime))

                .ForMember(item => item.Comment, options => options.MapFrom(item => item.Comment))
                .ForMember(item => item.Reader, options => options.MapFrom(item => item.Reader));
        }
    }
}
