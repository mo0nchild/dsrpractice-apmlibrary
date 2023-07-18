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
    public partial class CreateBookCommand : IRequest<int?>, IMappingWith<Publication>
    {
        public int PublisherId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string AuthorName { get; set; } = default!;

        public DateOnly YearIssue { get; set; } = default!;
        public string PublishType { get; set; } = default!;
        public string Language { get; set; } = default!;

        public string? Description { get; set; } = default!;
        public List<string> Genres { get; set; } = default!;

        public byte[] BookBody { get; set; } = default!;
        public byte[]? Image { get; set; } = null;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<CreateBookCommand, Publication>()
                .ForMember(item => item.PublisherId, options => options.MapFrom(item => item.PublisherId))
                .ForMember(item => item.Title, options => options.MapFrom(item => item.Title))
                .ForMember(item => item.AuthorName, options => options.MapFrom(item => item.AuthorName))

                .ForMember(item => item.Description, options => options.MapFrom(item => item.Description))
                .ForMember(item => item.YearIssue, options => options.MapFrom(item => item.YearIssue))
                .ForMember(item => item.Body, options => options.MapFrom(item => item.BookBody))
                .ForMember(item => item.Image, options => options.MapFrom(item => item.Image))

                .ForMember(item => item.Language, options => options.Ignore())
                .ForMember(item => item.Genres, options => options.Ignore());
        }
    }
}
