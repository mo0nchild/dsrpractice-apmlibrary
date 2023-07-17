using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.BookCommands
{
    public partial class SetBookmarkCommand : IRequest
    {
        public int PublishId { get; set; } = default!;
        public int ReaderId { get; set; } = default!;
    }
}
