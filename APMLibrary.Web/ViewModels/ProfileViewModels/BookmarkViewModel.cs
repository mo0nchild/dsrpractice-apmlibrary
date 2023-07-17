using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Models.ProfileModels;

namespace APMLibrary.Web.ViewModels.ProfileViewModels
{
    public partial class BookmarkViewModel : IMappingWith<BookmarkDto>
    {
        public int Id { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string AuthorName { get; set; } = default!;

        public string Description { get; set; } = default!;
        public int PageCount { get; set; } = default!;
        public double Rating { get; set; } = default!;
        public byte[]? Image { get; set; } = default;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<BookmarkDto, BookmarkViewModel>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Title, options => options.MapFrom(item => item.Title))
                .ForMember(item => item.AuthorName, options => options.MapFrom(item => item.AuthorName))

                .ForMember(item => item.Description, options => options.MapFrom(item => item.Description))
                .ForMember(item => item.PageCount, options => options.MapFrom(item => item.PageCount))
                .ForMember(item => item.Rating, options => options.MapFrom(item => item.Rating))
                .ForMember(item => item.Image, options => options.MapFrom(item => item.Image));
        }
    }
}
