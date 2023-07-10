using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.BookCommands.Validations
{
    public sealed class UpdateBookValidation : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookValidation() : base() 
        {
            this.RuleFor(item => item.BookId).GreaterThan(0).WithMessage("Неверное значение ИД издания");
            this.RuleFor(item => item.Title).NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100)
                .WithMessage("Неверное название издания");

            this.RuleFor(item => item.AuthorName).NotEmpty()
                .MinimumLength(5)
                .MaximumLength(50)
                .WithMessage("Неправильно указано имя автора");

            this.RuleFor(item => item.PublishType).NotEmpty()
                .MaximumLength(50)
                .WithMessage("Неправильно указан тип публикации");
            this.RuleFor(item => item.Language).NotEmpty()
                .MaximumLength(50)
                .WithMessage("Неправильно указан язык перевода публикации");

            this.RuleFor(item => item.Genres).NotEmpty()
                .Must(item => item.Count() > 0)
                .WithMessage("Неправильно заполнен список жанров");
        }
    }
}
