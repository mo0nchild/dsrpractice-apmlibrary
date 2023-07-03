using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace APMLibrary.Web.ViewModels
{
    public enum OrderType : sbyte
    {
        [Display(Name = "По дате публикации")]
        DateTime,

        [Display(Name = "По названию")]
        PublishTitle,

        [Display(Name = "По рейтингу")]
        RatingValue,

        [Display(Name = "По количеству страниц")]
        PageCount
    }
    public partial class FilterViewModel : object
    {
        [Required(ErrorMessage = "Не установлено значение языка перевода")]
        public string? Language { get; set; } = default!;

        public string? SearchingText { get; set; } = default!;

        public bool ForSubscriber { get; set; } = default!;
        public OrderType OrderType { get; set; } = default!;
    }
}
