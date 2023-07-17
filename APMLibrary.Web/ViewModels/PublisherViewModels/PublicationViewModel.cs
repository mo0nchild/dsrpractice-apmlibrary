using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Models.PublisherModels;
using APMLibrary.Dal.Entities;

namespace APMLibrary.Web.ViewModels.PublisherViewModels
{
    public partial class PublicationViewModel : IMappingWith<PublicationDto>
    {
        public int Id { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string AuthorName { get; set; } = default!;
        public int PageCount { get; set; } = default!;
        public string? Description { get; set; } = default;

        public string Language { get; set; } = default!;
        public string BookType { get; set; } = default!;
        public DateOnly Date { get; set; } = default!;
        public bool Published { get; set; } = default!;
        public double Rating { get; set; } = default!;
        public int CommentCount { get; set; } = default!;
        public byte[]? Image { get; set; } = null;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<PublicationDto, PublicationViewModel>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Title, options => options.MapFrom(item => item.Title))
                .ForMember(item => item.AuthorName, options => options.MapFrom(item => item.AuthorName))
                .ForMember(item => item.PageCount, options => options.MapFrom(item => item.PageCount))
                .ForMember(item => item.Description, options => options.MapFrom(item => item.Description))

                .ForMember(item => item.Date, options => options.MapFrom(item => item.Date))
                .ForMember(item => item.Published, options => options.MapFrom(item => item.Published))
                .ForMember(item => item.Language, options => options.MapFrom(item => item.Language))
                .ForMember(item => item.BookType, options => options.MapFrom(item => item.BookType))
                .ForMember(item => item.Rating, options => options.MapFrom(item => item.Rating))
                .ForMember(item => item.CommentCount, options => options.MapFrom(item => item.CommentCount))
                .ForMember(item => item.Image, options => options.MapFrom(item => item.Image));
        }
    }
}
