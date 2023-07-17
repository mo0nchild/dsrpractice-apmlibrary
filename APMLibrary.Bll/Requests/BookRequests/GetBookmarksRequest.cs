using APMLibrary.Bll.Models.BookModels;
using APMLibrary.Bll.Models.ProfileModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.BookRequests
{
    public partial class GetBookmarksRequest : IRequest<IEnumerable<BookmarkDto>>
    {
        public int ReaderId { get; set; } = default!;
        public GetBookmarksRequest(int readerId) => this.ReaderId = readerId;
    }
}
