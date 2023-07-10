using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Models;
using APMLibrary.Web.ViewModels.ProfileViewModels;

namespace APMLibrary.Web.ViewModels.BookViewModels
{
    public partial class RatingViewModel : IMappingWith<RatingDto>
    {
        public int Id { get; set; } = default!;
        public int Rating { get; set; } = default!;
        public DateOnly Date { get; set; } = default!;

        public string? Comment { get; set; } = default;
        public ProfileNavViewModel Reader { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<RatingDto, RatingViewModel>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Rating, options => options.MapFrom(item => item.Value))
                .ForMember(item => item.Date, options => options.MapFrom(item => item.Date))

                .ForMember(item => item.Comment, options => options.MapFrom(item => item.Comment))
                .ForMember(item => item.Reader, options => options.MapFrom(item => item.Reader));
        }
    }
}
