using APMLibrary.Bll.Models.BookModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Requests.BookRequests
{
    public partial class GetBookRequest : IRequest<BookDto?>
    {
        public int Id { get; set; } = default!;
        public GetBookRequest(int id) : base() => this.Id = id;
    }
}
