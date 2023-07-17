using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Dal.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.BookCommands
{
    public partial class SetRatingCommand : IRequest, IMappingWith<Rating>
    {
        public int PublishId { get; set; } = default!;

        public int ReaderId { get; set; } = default!;
        public string? Comment { get; set; } = default!;
        public int Rating { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<SetRatingCommand, Rating>()
                .ForMember(item => item.PublicationId, options => options.MapFrom(item => item.PublishId))
                .ForMember(item => item.ReaderId, options => options.MapFrom(item => item.ReaderId))
                .ForMember(item => item.Comment, options => options.MapFrom(item => item.Comment))

                .ForMember(item => item.Value, options => options.MapFrom(item => item.Rating))
                .ForMember(item => item.DateOnly, options => options.MapFrom(item => DateTime.UtcNow));
        }
    }
}
