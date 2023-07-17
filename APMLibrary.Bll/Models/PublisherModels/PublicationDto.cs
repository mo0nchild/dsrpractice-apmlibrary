using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Models.BookModels;
using APMLibrary.Dal.Entities;
using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Models.PublisherModels
{
    public partial class PublicationDto : IMappingWith<Publication>
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
            profile.CreateMap<ICollection<Rating>, double>().ConvertUsing((value, dist) =>
            {
                return value == null || value.Count == 0 ? default : value.Average(item => item.Value);
            });
            profile.CreateMap<Publication, PublicationDto>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Title, options => options.MapFrom(item => item.Title))
                .ForMember(item => item.AuthorName, options => options.MapFrom(item => item.AuthorName))
                .ForMember(item => item.PageCount, options => options.MapFrom(item => item.NumberPages))
                .ForMember(item => item.Description, options => options.MapFrom(item => item.Description))

                .ForMember(item => item.Date, options => options.MapFrom(item => item.YearIssue))
                .ForMember(item => item.Published, options => options.MapFrom(item => item.Published))
                .ForMember(item => item.Language, options => options.MapFrom(item => item.Language.Name))
                .ForMember(item => item.BookType, options => options.MapFrom(item => item.PublicationType.Name))
                .ForMember(item => item.Rating, options => options.MapFrom(item => item.Ratings))
                .ForMember(item => item.CommentCount, options => options.MapFrom(item => item.Ratings.Count()))
                .ForMember(item => item.Image, options => options.MapFrom(item => item.Image));
        }
    }
}
