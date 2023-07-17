using APMLibrary.Bll.Commands.BookCommands;
using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Models.BookModels;
using APMLibrary.Bll.Models.SupportModels;
using System.ComponentModel.DataAnnotations;

namespace APMLibrary.Web.ViewModels.AdminViewModels
{
    public partial class EditBookViewModel : IMappingWith<UpdateBookCommand>, IValidatableObject
    {
        public int BookId { get; set; } = default!;

        [Required(ErrorMessage = "Не указано название публикации")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Длина названия в диапазоне от 5 до 50 символов")]
        public string Title { get; set; } = default!;

        [Required(ErrorMessage = "Не указано имя автора")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина имени автора в диапазоне от 5 до 50 символов")]
        public string AuthorName { get; set; } = default!;

        [Required(ErrorMessage = "Дата издания не указана")]
        public DateTime YearIssue { get; set; } = default!;

        [Required(ErrorMessage = "Не указан тип публикации")]
        public string PublishType { get; set; } = default!;

        [Required(ErrorMessage = "Не указан язык перевода публикации")]
        public string Language { get; set; } = default!;

        [Required(ErrorMessage = "Не указано описание публикации")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Длина описания в диапазоне от 10 до 500 символов")]
        public string? Description { get; set; } = default;
        public List<string>? Genres { get; set; } = default!;

        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<EditBookViewModel, UpdateBookCommand>()
                .ForMember(item => item.BookId, options => options.MapFrom(item => item.BookId))
                .ForMember(item => item.Title, options => options.MapFrom(item => item.Title))
                .ForMember(item => item.AuthorName, options => options.MapFrom(item => item.AuthorName))

                .ForMember(item => item.YearIssue, options => options.MapFrom(item => DateOnly.FromDateTime(item.YearIssue)))
                .ForMember(item => item.PublishType, options => options.MapFrom(item => item.PublishType))
                .ForMember(item => item.Language, options => options.MapFrom(item => item.Language))

                .ForMember(item => item.Description, options => options.MapFrom(item => item.Description))
                .ForMember(item => item.Genres, options => options.MapFrom(item => item.Genres));
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var modelErrors = new List<ValidationResult>();
            if (this.Genres == null || this.Genres.Count <= 0)
            {
                modelErrors.Add(new ValidationResult("Список жанров публикации пуст", new string[] { "Genres" }));
            }
            return modelErrors;
        }
    }
}
