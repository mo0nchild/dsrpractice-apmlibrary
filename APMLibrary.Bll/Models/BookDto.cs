using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Models
{
    public partial class BookDto : IMappingWith<Publication>
    {
        public int Id { get; set; } = default!;
        public string VenderCode { get => string.Format("{0:D13}", Id); }

        public string Title { get; set; } = default!;
        public string AuthorName { get; set; } = default!;
        public int NumberPages { get; set; } = default!;
        public string? Description { get; set; } = default;

        public DateOnly YearIssue { get; set; } = default!;
        public bool Published { get; set; } = default!;

        public string Language { get; set; } = default!;
        public string PublicationType { get; set; } = default!;

        public byte[] Body { get; set; } = default!;
        public byte[]? FrontCover { get; set; } = null;
        public byte[]? BackCover { get; set; } = null;

        public ProfileDto Publisher { get; set; } = default!;
        public List<GenreDto> Genres { get; set; } = new();
        public List<RatingDto> Ratings { get; set; } = new();

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Publication, BookDto>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Title, options => options.MapFrom(item => item.Title))
                .ForMember(item => item.AuthorName, options => options.MapFrom(item => item.AuthorName))
                .ForMember(item => item.NumberPages, options => options.MapFrom(item => item.NumberPages))
                .ForMember(item => item.Description, options => options.MapFrom(item => item.Description))

                .ForMember(item => item.YearIssue, options => options.MapFrom(item => item.YearIssue))
                .ForMember(item => item.Published, options => options.MapFrom(item => item.Published))

                .ForMember(item => item.Language, options => options.MapFrom(item => item.Language.Name))
                .ForMember(item => item.PublicationType, options => options.MapFrom(item => item.PublicationType.Name))

                .ForMember(item => item.FrontCover, options => options.MapFrom(item => item.BookCover.FrontCover))
                .ForMember(item => item.BackCover, options => options.MapFrom(item => item.BookCover.BackCover))
                .ForMember(item => item.Body, options => options.MapFrom(item => item.Body))
                .ForMember(item => item.Publisher, options => options.MapFrom(item => item.Publisher))

                .ForMember(item => item.Genres, options => options.MapFrom(item => item.Genres))
                .ForMember(item => item.Ratings, options => options.MapFrom(item => item.Ratings));
        }
    }
}
