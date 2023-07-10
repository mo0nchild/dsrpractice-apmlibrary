using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.SupportCommand.Validations
{
    public sealed class GenreAddValidation : AbstractValidator<GenreAddCommand>
    {
        public GenreAddValidation() : base() 
        {
            this.RuleFor(item => item.Name).NotEmpty()
                .MaximumLength(50)
                .WithMessage("Неверно указано название жанра");
            this.RuleFor(item => item.Name).NotEmpty()
                .MaximumLength(50)
                .WithMessage("Неверно указана категория издания");
        }
    }
}
