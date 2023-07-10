using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Models;
using System.ComponentModel.DataAnnotations;

namespace APMLibrary.Web.ViewModels.ComponentsViewModels
{
    public partial class CatalogItemViewModel : IValidatableObject, IMappingWith<BookDto>
    {
        public int Id { get; set; } = default!;

        [Required(ErrorMessage = "Не установлено значение строки")]
        public string FirstLine { get; set; } = default!;
        public string? LastLine { get; set; } = default!;

        [Range(0, 5, ErrorMessage = "Неверное значение оценки публикации")]
        public double Rating { get; set; } = default!;
        public byte[]? Image { get; set; } = null;

        public int PageCount { get; set; } = default!;

        [Required(ErrorMessage = "Не установлено значение описания ")]
        public string Description { get; set; } = default!;

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResult = new List<ValidationResult>();
            if (string.IsNullOrEmpty(FirstLine))
            {
                validationResult.Add(new ValidationResult("Неверное значение строки", new string[] { "FirstLine" }));
            }
            return validationResult;
        }
        public virtual void ConfigureMapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<List<RatingDto>, double>().ConvertUsing((value, dist) =>
            {
                return value == null || value.Count == 0 ? default : value.Average(item => item.Value);
            });
            profile.CreateMap<BookDto, CatalogItemViewModel>()
                .ForMember(item => item.Id, options => options.MapFrom(item => item.Id))
                .ForMember(item => item.FirstLine, options => options.MapFrom(item => item.Title))
                .ForMember(item => item.LastLine, options => options.MapFrom(item => item.AuthorName))
                .ForMember(item => item.Rating, options => options.MapFrom(item => item.Ratings))

                .ForMember(item => item.Image, options => options.MapFrom(item => item.FrontCover))
                .ForMember(item => item.PageCount, options => options.MapFrom(item => item.NumberPages))
                .ForMember(item => item.Description, options => options.MapFrom(item => item.Description));
        }
    }

    public partial class BookCatalogViewModel : object
    {
        public int AllCount { get; set; } = default!;
        public string Name { get; set; } = default!;

        public IEnumerable<CatalogItemViewModel> Items { get; set; } = default!;
    }
}
