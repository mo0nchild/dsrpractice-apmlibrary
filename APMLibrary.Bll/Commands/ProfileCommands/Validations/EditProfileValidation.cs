using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.ProfileCommands.Validations
{
    public sealed class EditProfileValidation : AbstractValidator<EditProfileCommand>
    {
        public EditProfileValidation() : base()
        {
            this.RuleFor(item => item.Name).NotEmpty()
                .MaximumLength(50)
                .WithMessage("Неверно указано имя пользователя");
            this.RuleFor(item => item.Surname).NotEmpty()
                .MaximumLength(50)
                .WithMessage("Неверно указана фамилия пользователя");

            this.RuleFor(item => item.Email).NotEmpty()
                .MaximumLength(100)
                .Matches(@"^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$")
                .WithMessage("Неверный формат почты");

            this.RuleFor(item => item.Surname).NotEmpty()
                .Matches(@"^\+7[0-9]{10}$")
                .WithMessage("Неверный формат номера телефона");
        }
    }
}
