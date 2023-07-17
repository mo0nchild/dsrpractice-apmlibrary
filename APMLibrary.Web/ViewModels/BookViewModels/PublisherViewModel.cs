using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Models.ProfileModels;
using APMLibrary.Web.ViewModels.ComponentsViewModels;
using Microsoft.Extensions.Options;

namespace APMLibrary.Web.ViewModels.BookViewModels
{
    public partial class PublisherViewModel : IMappingWith<ProfileDto>
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Email { get; set; } = default!;
        public byte[]? Image { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<ProfileDto, PublisherViewModel>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.Name, options => options.MapFrom(item => item.Name))
                .ForMember(item => item.Surname, options => options.MapFrom(item => item.Surname))

                .ForMember(item => item.Email, options => options.MapFrom(item => item.Email))
                .ForMember(item => item.Image, options => options.MapFrom(item => item.Image));
        }
    }
}
