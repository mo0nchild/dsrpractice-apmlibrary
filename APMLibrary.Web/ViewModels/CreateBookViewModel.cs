using APMLibrary.Bll.Commands;
using APMLibrary.Bll.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace APMLibrary.Web.ViewModels
{
    public partial class CreateBookViewModel : object, IValidatableObject, IMappingWith<CreateProfileCommand>
    {
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
            if(this.Genres == null || this.Genres.Count <= 0)
            {
                modelErrors.Add(new ValidationResult("Список жанров публикации пуст", new string[] { "Genres" }));
            }
            return modelErrors;
        }
    }
}
