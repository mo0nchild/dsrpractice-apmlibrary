using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.BookCommands
{
    public partial class DeleteRatingCommand : IRequest
    {
        public int RatingId { get; set; } = default!;
        public DeleteRatingCommand(int id) : base() => this.RatingId = id;
    }
}
