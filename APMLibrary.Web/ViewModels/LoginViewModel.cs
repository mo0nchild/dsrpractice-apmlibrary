using APMLibrary.Bll.Commands;
using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Requests;
using System.ComponentModel.DataAnnotations;

namespace APMLibrary.Web.ViewModels
{
    public partial class LoginViewModel : object, IMappingWith<GetAuthorizationRequest>
    {
        [Required(ErrorMessage = "Не установлено значение для логина")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Длина пароля в диапазоне от 8 до 50 символов")]
        public string Login { get; set; } = default!;

        [Required(ErrorMessage = "Не установлено значение для пароля")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Длина пароля в диапазоне от 8 до 50 символов")]
        public string Password { get; set; } = default!;
        public bool RememberMe { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<LoginViewModel, GetAuthorizationRequest>()
                .ForMember(target => target.Login, sourse => sourse.MapFrom(item => item.Login))
                .ForMember(target => target.Password, sourse => sourse.MapFrom(item => item.Password));
        }
    }
}
