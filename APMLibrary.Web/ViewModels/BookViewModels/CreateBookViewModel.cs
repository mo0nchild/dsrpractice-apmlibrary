using APMLibrary.Bll.Commands.BookCommands;
using APMLibrary.Bll.Commands.ProfileCommands;
using APMLibrary.Bll.Common.Mappings;
using Org.BouncyCastle.Asn1.Ocsp;
using System.ComponentModel.DataAnnotations;

namespace APMLibrary.Web.ViewModels.BookViewModels
{
    public partial class CreateBookViewModel : object, IValidatableObject, IMappingWith<CreateBookCommand>
    {
        public int PublisherId { get; set; } = default!;

        [Required(ErrorMessage = "Не указано название публикации")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Длина названия в диапазоне от 5 до 50 символов")]
        public string Title { get; set; } = default!;

        [Required(ErrorMessage = "Не указано имя автора")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина имени автора в диапазоне от 5 до 50 символов")]
        public string AuthorName { get; set; } = default!;

        [Required(ErrorMessage = "Дата издания не указана")]
        public DateTime YearIssue { get; set; } = new();

        [Required(ErrorMessage = "Не указан тип публикации")]
        public string PublishType { get; set; } = default!;

        [Required(ErrorMessage = "Не указан язык перевода публикации")]
        public string Language { get; set; } = default!;

        [Required(ErrorMessage = "Не указано описание публикации")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Длина описания в диапазоне от 10 до 500 символов")]
        public string Description { get; set; } = default!;
        public List<string>? Genres { get; set; } = default!;

        [Required(ErrorMessage = "Файл издания не был установлен")]
        public IFormFile BookBody { get; set; } = default!;

        public IFormFile? FrontCover { get; set; } = default!;
        public IFormFile? BackCover { get; set; } = default!;

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var modelErrors = new List<ValidationResult>();
            if (Genres == null || Genres.Count <= 0)
            {
                modelErrors.Add(new ValidationResult("Список жанров публикации пуст", new string[] { "Genres" }));
            }
            return modelErrors;
        }

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<DateTime, DateOnly>().ConvertUsing((value, dest) => DateOnly.FromDateTime(value));
            profile.CreateMap<IFormFile?, byte[]?>().ConvertUsing((value, dest) =>
            {
                if (value == null) return null;

                var result = new byte[value.Length];
                using (var stream = value.OpenReadStream())
                {
                    stream.Read(result, 0, (int)value.Length);
                }
                return result;
            });
            profile.CreateMap<CreateBookViewModel, CreateBookCommand>()
                .ForMember(item => item.PublisherId, options => options.MapFrom(item => item.PublisherId))
                .ForMember(item => item.Title, options => options.MapFrom(item => item.Title))
                .ForMember(item => item.AuthorName, options => options.MapFrom(item => item.AuthorName))

                .ForMember(item => item.YearIssue, options => options.MapFrom(item => item.YearIssue))
                .ForMember(item => item.PublishType, options => options.MapFrom(item => item.PublishType))
                .ForMember(item => item.Language, options => options.MapFrom(item => item.Language))

                .ForMember(item => item.Genres, options => options.MapFrom(item => item.Genres))
                .ForMember(item => item.Description, options => options.MapFrom(item => item.Description))

                .ForMember(item => item.BookBody, options => options.MapFrom(item => item.BookBody))
                .ForMember(item => item.FrontCover, options => options.MapFrom(item => item.FrontCover))
                .ForMember(item => item.BackCover, options => options.MapFrom(item => item.BackCover));
        }
    }
}
