using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.ProfileCommands.Validations
{
    public sealed class CreateProfileValidation : AbstractValidator<CreateProfileCommand>
    {
        public CreateProfileValidation() : base()
        {
            this.RuleFor(item => item.Login).NotEmpty()
                .MaximumLength(50)
                .WithMessage("Неверно указан логин профиля");
            this.RuleFor(item => item.Password).NotEmpty()
                .MaximumLength(50)
                .WithMessage("Неверно указан пароль профиля");

            this.RuleFor(item => item.UserName).NotEmpty()
                .MaximumLength(50)
                .WithMessage("Неверно указано имя пользователя");
            this.RuleFor(item => item.Surname).NotEmpty()
                .MaximumLength(50)
                .WithMessage("Неверно указана фамилия пользователя");

            this.RuleFor(item => item.Email).NotEmpty()
                .MaximumLength(100)
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage("Неверный формат почты");

            this.RuleFor(item => item.Phone)
                .Must(item => item == null || Regex.IsMatch(item, @"^\+7[0-9]{10}$"))
                .WithMessage("Неверный формат номера телефона");
        }
    }
}
