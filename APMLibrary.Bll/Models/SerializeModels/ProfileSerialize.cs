using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace APMLibrary.Bll.Models.SerializeModels
{
    [XmlRoot(ElementName = "profile")]
    public partial class ProfileSerialize : IMappingWith<Profile>
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; } = default!;
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;
        public AccountType AccountType { get; set; } = default;

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; } = default!;

        [XmlAttribute(AttributeName = "surname")]
        public string Surname { get; set; } = default!;

        public string Email { get; set; } = default!;
        public string? Phone { get; set; } = default!;

        [XmlElement(DataType = "hexBinary")]
        public byte[]? Image { get; set; } = null;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Profile, ProfileSerialize>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Login, options => options.MapFrom(item => item.Authorization.Login))
                .ForMember(item => item.Password, options => options.MapFrom(item => item.Authorization.Password))
                .ForMember(item => item.AccountType, options => options.MapFrom(item => item.Authorization.AccountType))

                .ForMember(item => item.Name, options => options.MapFrom(item => item.Name))
                .ForMember(item => item.Surname, options => options.MapFrom(item => item.Surname))

                .ForMember(item => item.Email, options => options.MapFrom(item => item.Email))
                .ForMember(item => item.Phone, options => options.MapFrom(item => item.Phone))

                .ForMember(item => item.Image, options => options.MapFrom(item => item.Image))
                .ReverseMap();
        }
    }
}
