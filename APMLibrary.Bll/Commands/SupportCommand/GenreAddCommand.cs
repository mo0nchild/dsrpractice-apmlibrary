using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.SupportCommand
{
    public partial class GenreAddCommand : IRequest
    {
        public string Name { get; set; } = default!;
        public string Category { get; set; } = default!;
    }
}
