using System.ComponentModel.DataAnnotations;

namespace APMLibrary.Web.ViewModels
{
    public partial class CatalogItemViewModel : object, IValidatableObject
    {
        [Required(ErrorMessage = "Не установлено значение строки")]
        public string FirstLine { get; set; } = default!;
        public string? LastLine { get; set; } = default!;

        [Range(0, 5, ErrorMessage = "Неверное значение оценки публикации")]
        public double? Rating { get; set; } = default!;
        public byte[]? Image { get; set; } = default!;

        public int PageCount { get; set; } = default!;

        [Required(ErrorMessage = "Не установлено значение описания ")]
        public string Description { get; set; } = default!;

        public bool ForSubscriber { get; set; } = default!;

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResult = new List<ValidationResult>();
            if(string.IsNullOrEmpty(this.FirstLine))
            {
                validationResult.Add(new ValidationResult("Неверное значение строки", new string[] { "FirstLine" }));
            }
            return validationResult;
        }
    }

    public partial class BookCatalogViewModel : object
    {
        public int AllCount { get; set; } = default!;
        public string Name { get; set; } = default!;

        public List<CatalogItemViewModel> Items { get; set; } = new ();
    }
}
