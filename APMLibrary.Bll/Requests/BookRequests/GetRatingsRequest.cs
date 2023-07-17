using APMLibrary.Bll.Models.BookModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.BookRequests
{
    public partial class GetRatingsRequest : IRequest<RatingsListDto>
    {
        public int BookId { get; set; } = default!;
        public int Skip { get; set; } = default!;
        public int Take { get; set; } = default!;
    }
}
