using APMLibrary.Bll.Models;
using MediatR;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.BookRequests
{
    public enum SortingType : sbyte { ByDate, ByName, ByRating, ByPageCount }

    public partial class GetBooksListRequest : IRequest<BooksListDto>
    {
        public string? TextFilter { get; set; } = default;
        public string? LanguageFilter { get; set; } = default;
        public string? GenreFilter { get; set; } = default;
        public bool? IsPublished { get; set; } = default;

        public SortingType SortingType { get; set; } = default!;

        public int Skip { get; set; } = default!;
        public int Take { get; set; } = default!;
    }
}
