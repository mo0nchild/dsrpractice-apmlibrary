using APMLibrary.Bll.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.SupportRequests
{
    public partial class GetGenresRequest : IRequest<IEnumerable<GenreDto>>
    {
        public GetGenresRequest() : base() { }
    }
}
