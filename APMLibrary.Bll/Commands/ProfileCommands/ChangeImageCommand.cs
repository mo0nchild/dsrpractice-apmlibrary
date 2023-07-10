using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APMLibrary.Bll.Commands.ProfileCommands
{
    public partial class ChangeImageCommand : IRequest
    {
        public int Id { get; set; } = default!;
        public byte[] Image { get; set; } = default!;
    }
}
