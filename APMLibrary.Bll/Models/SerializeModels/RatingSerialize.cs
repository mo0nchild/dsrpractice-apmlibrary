using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace APMLibrary.Bll.Models.SerializeModels
{
    [XmlRoot(ElementName = "rating")]
    public partial class RatingSerialize : IMappingWith<Rating>
    {
        [XmlAttribute(AttributeName = "value")]
        public double Value { get; set; } = default!;
        public string? Comment { get; set; } = default!;

        [XmlAttribute(AttributeName = "datetime")]
        public DateTime DateTime { get; set; } = default!;

        public int ProfileId { get; set; } = default!;
        public int BookId { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Rating, RatingSerialize>()
                .ForMember(item => item.Value, options => options.MapFrom(item => item.Value))
                .ForMember(item => item.Comment, options => options.MapFrom(item => item.Comment))
                .ForMember(item => item.DateTime, options => options.MapFrom(item => item.DateOnly))

                .ForMember(item => item.ProfileId, options => options.MapFrom(item => item.ReaderId))
                .ForMember(item => item.BookId, options => options.MapFrom(item => item.PublicationId))
                .ReverseMap();
        }
    }
}
