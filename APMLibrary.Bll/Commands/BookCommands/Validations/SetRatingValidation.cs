using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.BookCommands.Validations
{
    public sealed class SetRatingValidation : AbstractValidator<SetRatingCommand>
    {
        public SetRatingValidation() : base()
        {
            this.RuleFor(item => item.Rating).NotEmpty()
                .Must(item => item <= 5 && item >= 0)
                .WithMessage("Неверное название издания");
        }
    }
}
