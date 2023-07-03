using APMLibrary.Bll.Commands;
using APMLibrary.Bll.Common.Mappings;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace APMLibrary.Web.ViewModels
{
    public partial class RegistrationViewModel : object, IValidatableObject, IMappingWith<CreateProfileCommand>
    {
        [Required(ErrorMessage = "Не установлено значение имени")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Длина пароля в диапазоне от 4 до 50 символов")]
        public string Surname { get; set; } = default!;

        [Required(ErrorMessage = "Не установлено значение фамилии")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Длина пароля в диапазоне от 4 до 50 символов")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Не установлено значение электронной почты")]
        [RegularExpression(@"^\w{6,}@(mail|gmail|yandex){1}.(ru|com){1}$", ErrorMessage = "Неверный формат почты")]
        public string Email { get; set; } = default!;
        public string? Phone { get; set; } = default!;
        public LoginViewModel Authorization { get; set; } = default!;

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResult = new List<ValidationResult>();
            if(this.Phone != null && !Regex.IsMatch(this.Phone, @"^\+7[0-9]{10}$")) 
            {
                validationResult.Add(new ValidationResult("Неверный формат номера телефона", new string[] { "Phone" }));
            }
            return validationResult;
        }
        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<RegistrationViewModel, CreateProfileCommand>()
                .ForMember(target => target.UserName, sourse => sourse.MapFrom(item => item.Name))
                .ForMember(target => target.Surname, sourse => sourse.MapFrom(item => item.Surname))
                .ForMember(target => target.Email, sourse => sourse.MapFrom(item => item.Email))
                .ForMember(target => target.Phone, sourse => sourse.MapFrom(item => item.Phone))

                .ForMember(target => target.Login, sourse => sourse.MapFrom(item => item.Authorization.Login))
                .ForMember(target => target.Password, sourse => sourse.MapFrom(item => item.Authorization.Password));
        }
    }
}
