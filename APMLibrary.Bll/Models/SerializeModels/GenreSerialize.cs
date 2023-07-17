using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using Org.BouncyCastle.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace APMLibrary.Bll.Models.SerializeModels
{
    [XmlRoot(ElementName = "genre")]
    public partial class GenreSerialize : IMappingWith<Genre>
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; } = default!;

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; } = default!;
        public string Category { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Genre, GenreSerialize>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Name, options => options.MapFrom(item => item.Name))
                .ForMember(item => item.Category, options => options.MapFrom(item => item.Category.Name))
                .ReverseMap();
        }
    }
}
