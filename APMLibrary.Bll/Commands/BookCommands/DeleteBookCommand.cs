using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.BookCommands
{
    public partial class DeleteBookCommand : IRequest
    {
        public int Id { get; set; } = default!;
        public DeleteBookCommand(int id) : base() => this.Id = id;
    }
}
