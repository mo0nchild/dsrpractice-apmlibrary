using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace APMLibrary.Bll.Models.SerializeModels
{
    [XmlRoot(ElementName = "book")]
    public partial class BookSerialize : IMappingWith<Publication>
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; } = default!;

        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; } = default!;
        public string? Description { get; set; } = default;

        [XmlAttribute(AttributeName = "author")]
        public string AuthorName { get; set; } = default!;

        [XmlAttribute(AttributeName = "published")]
        public bool IsPublished { get; set; } = default!;
        public DateTime Date { get; set; } = default!;

        [XmlElement(DataType = "hexBinary")]
        public byte[]? Image { get; set; } = null;

        [XmlElement(DataType = "hexBinary")]
        public byte[] Body { get; set; } = default!;

        public int PublisherId { get; set; } = default!;
        public string Language { get; set; } = default!;
        public string BookType { get; set; } = default!;
        public List<string> Genres { get; set; } = new();
        public List<int> Readers { get; set; } = new();

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Publication, BookSerialize>()
                .ForMember(item => item.Title, options => options.MapFrom(item => item.Title))
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Description, options => options.MapFrom(item => item.Description))
                .ForMember(item => item.AuthorName, options => options.MapFrom(item => item.AuthorName))

                .ForMember(item => item.Date, options => options.MapFrom(item => item.YearIssue.ToDateTime(TimeOnly.MinValue)))
                .ForMember(item => item.Image, options => options.MapFrom(item => item.Image))
                .ForMember(item => item.Body, options => options.MapFrom(item => item.Body))
                .ForMember(item => item.IsPublished, options => options.MapFrom(item => item.Published))

                .ForMember(item => item.PublisherId, options => options.MapFrom(item => item.PublisherId))
                .ForMember(item => item.Language, options => options.MapFrom(item => item.Language.Name))
                .ForMember(item => item.BookType, options => options.MapFrom(item => item.PublicationType.Name))
                .ForMember(item => item.Genres, options => options.MapFrom(item => item.Genres.Select(item => item.Name)))
                .ForMember(item => item.Readers, options => options.MapFrom(item => item.Readers.Select(item => item.Id)))
                .ReverseMap();
        }

    }
}
