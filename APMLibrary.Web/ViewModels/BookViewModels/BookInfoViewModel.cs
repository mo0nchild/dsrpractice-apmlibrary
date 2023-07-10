using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Models;
using APMLibrary.Web.ViewModels.SupportViewModels;
using Microsoft.AspNetCore.Razor.Hosting;
using Org.BouncyCastle.Crypto.Paddings;

namespace APMLibrary.Web.ViewModels.BookViewModels
{
    public partial class BookInfoViewModel : IMappingWith<BookDto>
    {
        public int Id { get; set; } = default;
        public string Title { get; set; } = default!;
        public string AuthorName { get; set; } = default!;
        public string VenderCode { get; set; } = default!;

        public int PageCount { get; set; } = default!;
        public DateOnly Date { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public double Rating { get; set; } = default!;
        public byte[]? Image { get; set; } = default!;

        public string BookType { get; set; } = default!;
        public string Language { get; set; } = default!;

        public List<GenreViewModel> Genres { get; set; } = default!;
        public PublisherViewModel Publisher { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<List<RatingDto>, double>().ConvertUsing((value, dist) =>
            {
                return value == null || value.Count == 0 ? default : value.Average(item => item.Value);
            });
            profile.CreateMap<BookDto, BookInfoViewModel>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.VenderCode, options => options.MapFrom(item => item.VenderCode))
                .ForMember(item => item.Title, options => options.MapFrom(item => item.Title))
                .ForMember(item => item.AuthorName, options => options.MapFrom(item => item.AuthorName))

                .ForMember(item => item.PageCount, options => options.MapFrom(item => item.NumberPages))
                .ForMember(item => item.Date, options => options.MapFrom(item => item.YearIssue))
                .ForMember(item => item.Description, options => options.MapFrom(item => item.Description))
                .ForMember(item => item.Rating, options => options.MapFrom(item => item.Ratings))

                .ForMember(item => item.BookType, options => options.MapFrom(item => item.PublicationType))
                .ForMember(item => item.Language, options => options.MapFrom(item => item.Language))
                .ForMember(item => item.Genres, options => options.MapFrom(item => item.Genres))
                .ForMember(item => item.Image, options => options.MapFrom(item => item.FrontCover))

                .ForMember(item => item.Publisher, options => options.MapFrom(item => item.Publisher));
        }
    }
}
