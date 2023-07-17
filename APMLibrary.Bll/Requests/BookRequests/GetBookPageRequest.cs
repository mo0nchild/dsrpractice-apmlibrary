using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.BookRequests
{
    public partial class GetBookPageRequest : IRequest<string?>
    {
        public int BookId { get; set; } = default!;
        public int Page { get; set; } = default!;

        public GetBookPageRequest(int page, int bookId) : base()
        {
            this.Page = page;
            this.BookId = bookId;
        }
    }
}
