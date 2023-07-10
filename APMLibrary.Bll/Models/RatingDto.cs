using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Models
{
    public partial class RatingDto : IMappingWith<Rating>
    {
        public int Id { get; set; } = default!;
        public int Value { get; set; } = default!;

        public DateOnly Date { get; set; } = default!;
        public string? Comment { get; set; } = default;

        public ProfileDto Reader { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Rating, RatingDto>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Value, options => options.MapFrom(item => item.Value))
                .ForMember(item => item.Reader, options => options.MapFrom(item => item.Reader))

                .ForMember(item => item.Date, options => options.MapFrom(item => item.DateOnly))
                .ForMember(item => item.Comment, options => options.MapFrom(item => item.Comment));
        }
    }
}
