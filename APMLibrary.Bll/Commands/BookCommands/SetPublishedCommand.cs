using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.BookCommands
{
    public partial class SetPublishedCommand : IRequest
    {
        public int BookId { get; set; } = default!;
        public bool IsPublished { get; set; } = default!;
    }
}
