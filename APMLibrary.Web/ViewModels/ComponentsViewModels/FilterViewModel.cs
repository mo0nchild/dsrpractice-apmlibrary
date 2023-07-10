using APMLibrary.Bll.Common.Mappings;
using APMLibrary.Bll.Requests.BookRequests;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace APMLibrary.Web.ViewModels.ComponentsViewModels
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
    public partial class FilterViewModel : object, IMappingWith<GetBooksListRequest>
    {
        [Required(ErrorMessage = "Не установлено значение языка перевода")]
        public string? Language { get; set; } = default!;
        public string? SearchingText { get; set; } = default!;
        public OrderType OrderType { get; set; } = default!;

        public virtual void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<OrderType, SortingType>().ConvertUsing((value, dest) =>
                value switch
                {
                    OrderType.DateTime => SortingType.ByDate,
                    OrderType.PublishTitle => SortingType.ByName,

                    OrderType.PageCount => SortingType.ByPageCount,
                    OrderType.RatingValue => SortingType.ByRating,
                    _ => throw new InvalidOperationException(),
                });
        }
    }
}
