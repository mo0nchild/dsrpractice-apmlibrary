using APMLibrary.Bll.Commands.ProfileCommands;
using APMLibrary.Bll.Common.Mappings;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace APMLibrary.Web.ViewModels.ProfileViewModels
{
    public partial class EditProfileViewModel : object, IValidatableObject, IMappingWith<EditProfileCommand>
    {
        [Required(ErrorMessage = "Не установлено значение имени")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Длина пароля в диапазоне от 4 до 50 символов")]
        public string Surname { get; set; } = default!;

        [Required(ErrorMessage = "Не установлено значение фамилии")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Длина пароля в диапазоне от 4 до 50 символов")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Не установлено значение электронной почты")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Неверный формат почты")]
        public string Email { get; set; } = default!;
        public string? Phone { get; set; } = default!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResult = new List<ValidationResult>();
            if (this.Phone != null && !Regex.IsMatch(this.Phone, @"^\+7[0-9]{10}$"))
            {
                validationResult.Add(new ValidationResult("Неверный формат номера телефона", new string[] { "Phone" }));
            }
            return validationResult;
        }
        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<EditProfileViewModel, EditProfileCommand>()
                .ForMember(item => item.Surname, options => options.MapFrom(item => item.Surname))
                .ForMember(item => item.Name, options => options.MapFrom(item => item.Name))

                .ForMember(item => item.Email, options => options.MapFrom(item => item.Email))
                .ForMember(item => item.Phone, options => options.MapFrom(item => item.Phone));
        }
    }
}
