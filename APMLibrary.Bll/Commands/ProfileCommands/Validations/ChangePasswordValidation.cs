using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.ProfileCommands.Validations
{
    public sealed class ChangePasswordValidation : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidation() : base()
        {
            this.RuleFor(item => item.OldPassword).NotEmpty()
                .MaximumLength(50)
                .WithMessage("Неверно указан старый пароль профиля");
            this.RuleFor(item => item.OldPassword).NotEmpty()
                .MaximumLength(50)
                .WithMessage("Неверно указан новый пароль профиля");
        }
    }
}
